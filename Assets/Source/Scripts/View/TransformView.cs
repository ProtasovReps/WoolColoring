using System.Collections;
using UnityEngine;

public class TransformView : MonoBehaviour
{
    private Transform _transform;
    private Quaternion _startRotation;
    private Vector3 _startPosition;
    private Coroutine _coroutine;

    public void Initialize()
    {
        _transform = transform;
        _startRotation = _transform.rotation;
        _startPosition = _transform.position;
    }

    public void SetStartTransform()
    {
        _transform.position = _startPosition;
        _transform.rotation = _startRotation;
    }

    public void ChangePosition(Vector3 position, float moveSpeed, Collider[] colliders)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(MoveSmoothly(position, moveSpeed, colliders));
    }

    private IEnumerator MoveSmoothly(Vector3 position, float moveSpeed, Collider[] colliders)
    {
        float minDistance = 0.01f;

        SetColliderEnableState(false, colliders);

        while (GetSquareMagnitude(position) > minDistance)
        {
            _transform.position = Vector3.Lerp(_transform.position, position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        SetColliderEnableState(true, colliders);

        _coroutine = null;
    }

    private void SetColliderEnableState(bool isActive, Collider[] colliders)
    {
        for (int i = 0; i < colliders.Length; i++)
            colliders[i].enabled = isActive;
    }

    private float GetSquareMagnitude(Vector3 position)
    {
        Vector3 offset = position - _transform.position;
        return Vector3.SqrMagnitude(offset);
    }
}
