using System.Collections;
using UnityEngine;

public class TransformMoveView : TransformView
{
    private Coroutine _coroutine;
    private float _minDistance = 0.01f;

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
        SetColliderEnableState(false, colliders);

        while (GetSquareMagnitude(position, Transform.position) > _minDistance)
        {
            Transform.position = Vector3.Lerp(Transform.position, position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        SetColliderEnableState(true, colliders);

        _coroutine = null;
    }

    private IEnumerator MoveSmoothly(Transform comparer, Vector3 targetPosition, float moveSpeed)
    {
        while (GetSquareMagnitude(targetPosition, comparer.position, out Vector3 offset) > _minDistance)
        {
            Vector3 finalPosition = Transform.position + new Vector3(0f, offset.y, 0f);

            Transform.position = Vector3.Lerp(Transform.position, finalPosition, moveSpeed * Time.deltaTime);
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
