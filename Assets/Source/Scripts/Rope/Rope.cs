using System;
using System.Collections;
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

    private float _reconnectAutodestroyTime = 0.3f;
    private WaitForSeconds _appearDelay;
    private WaitForSeconds _disappearDelay;
    private ColorView _colorView;
    private LineRenderer _lineRenderer;
    private Coroutine _coroutine;
    private Transform _lastStartPoint;

    public event Action<Rope> Disconected;

    public Color Color => _colorView.Color;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _colorView = GetComponent<ColorView>();
        _appearDelay = new WaitForSeconds(_segmentAppearDelay);
        _disappearDelay = new WaitForSeconds(_segmentsDisappearDelay);

        _colorView.Initialize();
    }

    public void SetColor(Color color) => _colorView.SetColor(color);

    public void Connect(Transform startPosition, Transform endPosition)
    {
        _lastStartPoint = startPosition;
        _coroutine = StartCoroutine(ConnectAnimated(startPosition, endPosition));
    }

    public void Reconnect(Transform endPosition)
    {
        if (_lastStartPoint == null)
            throw new InvalidOperationException();

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ReconnectAnimated(endPosition));
    }

    public void Disconnect()
    {
        StopCoroutine(_coroutine);
        StartCoroutine(DisappearAnimated());
    }

    private IEnumerator ConnectAnimated(Transform startPosition, Transform endPosition)
    {
        int firstIndex = 1;
        int lastIndex = _segmentsCount - 1;

        yield return AppearAnimated(startPosition.position, endPosition.position, firstIndex, lastIndex);

        while (true)
        {
            _lineRenderer.SetPosition(0, startPosition.position);

            for (int i = firstIndex; i < lastIndex; i++)
            {
                SetPosition(startPosition.position, endPosition.position, i);
                yield return null;
            }

            _lineRenderer.SetPosition(lastIndex, endPosition.position);
        }
    }

    private IEnumerator AppearAnimated(Vector3 startPosition, Vector3 endPosition, int firstIndex, int lastIndex)
    {
        _lineRenderer.positionCount = 0;

        for (int i = 0; i < _segmentsCount; i++)
        {
            _lineRenderer.positionCount++;
            SetPosition(startPosition, endPosition, i);
            yield return _appearDelay;
        }
    }

    private IEnumerator ReconnectAnimated(Transform endPosition)
    {
        int firstIndex = 1;
        int lastIndex = _segmentsCount - 1;
        float elapsedTime = 0f;

        _lineRenderer.positionCount = _segmentsCount;

        while (elapsedTime < _reconnectAutodestroyTime)
        {
            _lineRenderer.SetPosition(0, _lastStartPoint.position);

            for (int i = firstIndex; i < lastIndex; i++)
            {
                SetPosition(_lastStartPoint.position, endPosition.position, i);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _lineRenderer.SetPosition(lastIndex, endPosition.position);
        }

        Disconnect();
    }

    private IEnumerator DisappearAnimated()
    {
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            _lineRenderer.positionCount--;
            yield return _disappearDelay;
        }

        _coroutine = null;
        Disconected?.Invoke(this);
    }

    private void SetPosition(Vector3 startPosition, Vector3 endPosition, int segmentNumber)
    {
        var position = Vector3.Lerp(startPosition, endPosition, (segmentNumber + 1f) / _segmentsCount);
        float randomizedPositionX = Random.Range(position.x - _maxOffset, position.x + _maxOffset);

        position = new Vector3(randomizedPositionX, position.y, position.z);

        _lineRenderer.SetPosition(segmentNumber, position);
    }
}
