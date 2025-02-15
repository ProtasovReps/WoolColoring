using System.Collections;
using UnityEngine;

public class TransformMoveView : MonoBehaviour
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

    public void ChangePosition(Vector3 targetPosition, Collider[] colliders, float moveSpeed)
    {
        ValidateCoroutine();

        _coroutine = StartCoroutine(MoveSmoothly(targetPosition, colliders, moveSpeed));
    }

    public void ChangePosition(Transform comparer, Vector3 targetPosition, float moveSpeed)
    {
        ValidateCoroutine();

        _coroutine = StartCoroutine(MoveSmoothly(comparer, targetPosition, moveSpeed));
    }

    private IEnumerator MoveSmoothly(Vector3 position, Collider[] colliders, float moveSpeed)
    {
        float minDistance = 0.01f;

        SetColliderEnableState(false, colliders);

        while (GetSquareMagnitude(position, _transform.position) > minDistance)
        {
            _transform.position = Vector3.Lerp(_transform.position, position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        SetColliderEnableState(true, colliders);

        _coroutine = null;
    }

    private IEnumerator MoveSmoothly(Transform comparer, Vector3 targetPosition, float moveSpeed)
    {
        float minDistance = 0.01f;

        while (GetSquareMagnitude(targetPosition, comparer.position, out Vector3 offset) > minDistance)
        {
            Vector3 finalPosition = _transform.position + new Vector3(0f, offset.y, 0f);

            _transform.position = Vector3.Lerp(_transform.position, finalPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }

        _coroutine = null;
    }

    private void ValidateCoroutine()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
    }

    private void SetColliderEnableState(bool isActive, Collider[] colliders)
    {
        for (int i = 0; i < colliders.Length; i++)
            colliders[i].enabled = isActive;
    }

    private float GetSquareMagnitude(Vector3 targetPosition, Vector3 currentPosition)
    {
        Vector3 offset = targetPosition - currentPosition;
        return Vector3.SqrMagnitude(offset);
    }

    private float GetSquareMagnitude(Vector3 targetPosition, Vector3 currentPosition, out Vector3 offset)
    {
        offset = targetPosition - currentPosition;
        return Vector3.SqrMagnitude(offset);
    }
}
