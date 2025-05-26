using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using CustomInterface;
using Extensions.View;

namespace ConnectingRope
{
    [RequireComponent(typeof(ColorView))]
    [RequireComponent(typeof(LineRenderer))]
    public class Rope : MonoBehaviour, IColorSettable
    {
        [SerializeField] private int _segmentsCount;
        [SerializeField] private float _segmentAppearDelay;
        [SerializeField] private float _segmentsDisappearDelay;
        [SerializeField] private float _maxOffset;
        [SerializeField] private float _waveSpeed;

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

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
        }

        public void SetColor(Color color)
        {
            _colorView.SetColor(color);
        }

        public void Connect(Transform startPosition, Transform endPosition)
        {
            _lastStartPoint = startPosition;
            _task = ConnectAnimated(startPosition, endPosition);
        }

        public void Reconnect(Transform endPosition)
        {
            if (_lastStartPoint == null)
                throw new InvalidOperationException();

            ValidateTask();
            _task = ReconnectAnimated(endPosition);
        }

        public async UniTaskVoid Disconnect()
        {
            ValidateTask();
            await DisappearAnimated();
        }

        private async UniTask ConnectAnimated(Transform startPosition, Transform endPosition)
        {
            await AppearAnimated(startPosition.position, endPosition.position);
            _task = UpdatePositions(startPosition, endPosition);
        }

        private async UniTask ReconnectAnimated(Transform endPosition)
        {
            _lineRenderer.positionCount = _segmentsCount;
            await UpdatePositions(_lastStartPoint, endPosition);
        }

        private async UniTask AppearAnimated(Vector3 startPosition, Vector3 endPosition)
        {
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
                    await UniTask.Yield(cancellationToken: _cancellationTokenSource.Token, cancelImmediately: true);
                }

                _lineRenderer.SetPosition(lastIndex, endPosition.position);
            }
        }

        private void SetPosition(Vector3 startPosition, Vector3 endPosition, int segmentNumber)
        {
            float segmentRealNumber = segmentNumber + 1f;
            var position = Vector3.Lerp(startPosition, endPosition, segmentRealNumber / _segmentsCount);
            float wavedPosition = Mathf.Sin(Time.time * _waveSpeed + segmentRealNumber) * _maxOffset;

            position = new Vector3(position.x + wavedPosition, position.y, position.z);

            _lineRenderer.SetPosition(segmentNumber, position);
        }

        private async UniTask DisappearAnimated()
        {
            for (int i = 0; i < _lineRenderer.positionCount; i++)
            {
                _lineRenderer.positionCount--;
                await UniTask.WaitForSeconds(_segmentsDisappearDelay);
            }

            Disconected?.Invoke(this);
        }

        private void ValidateTask()
        {
            if (_task.Status.IsCompleted() || _task.Status.IsCanceled())
                return;

            _cancellationTokenSource?.Cancel();
            _task.Forget();
        }
    }
}