using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(ColorView))]
[RequireComponent(typeof(LineRenderer))]
public class Rope : MonoBehaviour, IColorSettable
{
    [SerializeField] private int _segmentsCount;
    [SerializeField] private float _maxOffset;

    private ColorView _color;
    private LineRenderer _lineRenderer;
    private Coroutine _coroutine;

    public event Action<Rope> Disconected;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _color = GetComponent<ColorView>();

        _lineRenderer.positionCount = _segmentsCount;
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

        Disconected?.Invoke(this);
    }

    private IEnumerator ConnectAnimated(Transform startPosition, Transform endPosition)
    {
        while (true)
        {
            SetPositions(startPosition.position, endPosition.position);
            yield return null;
        }
    }

    private void SetPositions(Vector3 startPosition, Vector3 endPosition)
    {
        int firstIndex = 1;
        int lastIndex = _segmentsCount - 1;

        _lineRenderer.SetPosition(0, startPosition);
        _lineRenderer.SetPosition(lastIndex, endPosition);

        for (int i = firstIndex; i < lastIndex; i++)
        {
            var position = Vector3.Lerp(startPosition, endPosition, (i + 1f) / _lineRenderer.positionCount);
            float randomizedPositionX = Random.Range(position.x - _maxOffset, position.x + _maxOffset);

            position = new Vector3(randomizedPositionX, position.y, position.z);

            _lineRenderer.SetPosition(i, position);
        }
    }
}
