using UnityEngine;

public class TransformView : MonoBehaviour
{
    private Quaternion _startRotation;
    private Vector3 _startPosition;
    private Vector3 _startScale;
    private Transform _transform;

    public Vector3 StartScale => _startScale;
    public Transform Transform => _transform;

    public void Initialize()
    {
        _transform = transform;
        _startRotation = transform.rotation;
        _startPosition = transform.position;
        _startScale = transform.localScale;
    }

    public void SetStartTransform()
    {
        _transform.position = _startPosition;
        _transform.rotation = _startRotation;
        _transform.localScale = _startScale;
    }
}
