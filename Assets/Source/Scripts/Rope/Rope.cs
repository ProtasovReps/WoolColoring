using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ColorView))]
[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour, IColorSettable
{
    [SerializeField] private int _segmentsCount;
    [SerializeField] private float _segmentAppearDelay;
    [SerializeField] private float _segmentsDisappearDelay;
    [SerializeField] private float _maxOffset;

    private ColorView _colorView;
    private LineRenderer _lineRenderer;
    private UniTask _task;
    private CancellationTokenSource _cancellationTokenSource;
    private Transform _lastStartPoint;

    public event Action<Rope> Disconected;

    public Color Color => _colorView.Color;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _colorView = GetComponent<ColorView>();

        _colorView.Initialize();
    }

    public void SetColor(Color color) => _colorView.SetColor(color);

    public void Connect(Transform startPosition, Transform endPosition)
    {
        _lastStartPoint = startPosition;
        _task = ConnectAnimated(startPosition, endPosition);
    }

    public async UniTaskVoid Reconnect(Transform endPosition)
    {
        if (_lastStartPoint == null)
            throw new InvalidOperationException();

        await ValidateTask();
        _task = ReconnectAnimated(endPosition);
    }

    public async UniTaskVoid Disconnect()
    {
        await ValidateTask();
        DisappearAnimated().Forget();
    }

    private async UniTask ConnectAnimated(Transform startPosition, Transform endPosition)
    {
        await AppearAnimated(startPosition.position, endPosition.position);
        await UpdatePositions(startPosition, endPosition);
    }

    private async UniTask ReconnectAnimated(Transform endPosition)
    {
        _lineRenderer.positionCount = _segmentsCount;
        await UpdatePositions(_lastStartPoint, endPosition);
    }

    private async UniTask AppearAnimated(Vector3 startPosition, Vector3 endPosition)
    {
        int lastIndex = _segmentsCount - 1;

        _lineRenderer.positionCount = 0;

        for (int i = 0; i < _segmentsCount; i++)
        {
            _lineRenderer.positionCount++;
            SetPosition(startPosition, endPosition, i);
            await UniTask.WaitForSeconds(_segmentAppearDelay);
        }
    }

    private async UniTask UpdatePositions(Transform startPosition, Transform endPosition)
    {
        int firstIndex = 1;
        int lastIndex = _segmentsCount - 1;
        _cancellationTokenSource = new CancellationTokenSource();

        while (_cancellationTokenSource.IsCancellationRequested == false)
        {
            _lineRenderer.SetPosition(0, startPosition.position);

            for (int i = firstIndex; i < lastIndex; i++)
            {
                SetPosition(startPosition.position, endPosition.position, i);
                await UniTask.Yield();
            }

            _lineRenderer.SetPosition(lastIndex, endPosition.position);
        }
    }

    private void SetPosition(Vector3 startPosition, Vector3 endPosition, int segmentNumber)
    {
        var position = Vector3.Lerp(startPosition, endPosition, (segmentNumber + 1f) / _segmentsCount);
        float randomizedPositionX = Random.Range(position.x - _maxOffset, position.x + _maxOffset);

        position = new Vector3(randomizedPositionX, position.y, position.z);

        _lineRenderer.SetPosition(segmentNumber, position);
    }

    private async UniTaskVoid DisappearAnimated()
    {
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            _lineRenderer.positionCount--;
            await UniTask.WaitForSeconds(_segmentsDisappearDelay);
        }

        Disconected?.Invoke(this);
    }

    private async UniTask ValidateTask()
    {
        if (_task.Status.IsCompleted() || _task.Status.IsCanceled())
            return;

        _cancellationTokenSource.Cancel();
        await _task;

        _cancellationTokenSource.Dispose();
    }
}