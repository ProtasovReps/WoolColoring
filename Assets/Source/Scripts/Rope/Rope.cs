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

    private WaitForSeconds _appearDelay;
    private WaitForSeconds _disappearDelay;
    private ColorView _color;
    private LineRenderer _lineRenderer;
    private Coroutine _coroutine;

    public event Action<Rope> Disconected;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _color = GetComponent<ColorView>();
        _appearDelay = new WaitForSeconds(_segmentAppearDelay);
        _disappearDelay = new WaitForSeconds(_segmentsDisappearDelay);

        _color.Initialize();
    }

    public void SetColor(Color color) => _color.SetColor(color);

    public void Connect(Transform startPosition, Transform endPosition)
    {
        _coroutine = StartCoroutine(ConnectAnimated(startPosition, endPosition));
    }

    public void Disconect()
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

    private IEnumerator DisappearAnimated()
    {
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            _lineRenderer.positionCount--;
            yield return _disappearDelay;
        }

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
