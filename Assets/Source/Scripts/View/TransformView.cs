using System;
using System.Collections;
using UnityEngine;

public class TransformView : MonoBehaviour
{
    private Coroutine _coroutine;
    private Transform _transform;
    private Vector3 _startPosition;
    private Collider _collider;
    private Quaternion _startRotation;

    public void Initialize(Collider collider)
    {
        if(collider == null)
            throw new ArgumentNullException(nameof(collider));

        _collider = collider;
        _transform = transform;
        _startRotation = _transform.rotation;
        _startPosition = _transform.position;
    }

    public void SetStartTransform()
    {
        _transform.position = _startPosition;
        _transform.rotation = _startRotation;
    }

    public void SetActive(bool isActive)
    {
        _transform.gameObject.SetActive(isActive);
    }

    public void ChangePosition(Vector3 position, float moveSpeed)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(MoveSmoothly(position, moveSpeed));
    }

    private IEnumerator MoveSmoothly(Vector3 position, float moveSpeed)
    {
        float minDistance = 0.01f;

        _collider.enabled = false;

        while (GetSquareMagnitude(position) > minDistance)
        {
            _transform.position = Vector3.Lerp(_transform.position, position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        _collider.enabled = true;
        _coroutine = null;
    }

    private float GetSquareMagnitude(Vector3 position)
    {
        Vector3 offset = position - _transform.position;
        return Vector3.SqrMagnitude(offset);
    }
}
