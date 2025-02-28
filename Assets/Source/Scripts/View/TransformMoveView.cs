using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class TransformMoveView : TransformView
{
    private UniTask _task;
    private CancellationTokenSource _cancellationTokenSource;
    private float _minDistance = 0.01f;

    public void ChangePosition(Vector3 targetPosition, Collider[] colliders, float moveSpeed)
    {
        ValidateTask();

        _cancellationTokenSource = new CancellationTokenSource();
        _task = MoveSmoothly(targetPosition, colliders, moveSpeed);
    }

    public void ChangePosition(Transform comparer, Vector3 targetPosition, float moveSpeed)
    {
        ValidateTask();

        _cancellationTokenSource = new CancellationTokenSource();
        _task = MoveSmoothly(comparer, targetPosition, moveSpeed);
    }

    private async UniTask MoveSmoothly(Vector3 position, Collider[] colliders, float moveSpeed)
    {
        SetColliderEnableState(false, colliders);

        while (GetSquareMagnitude(position, Transform.position) > _minDistance && _cancellationTokenSource.IsCancellationRequested == false)
        {
            Transform.position = Vector3.Lerp(Transform.position, position, moveSpeed * Time.deltaTime);
            await UniTask.Yield();
        }

        SetColliderEnableState(true, colliders);
    }

    private async UniTask MoveSmoothly(Transform comparer, Vector3 targetPosition, float moveSpeed)
    {
        while (GetSquareMagnitude(targetPosition, comparer.position, out Vector3 offset) > _minDistance && _cancellationTokenSource.IsCancellationRequested == false)
        {
            Vector3 finalPosition = Transform.position + new Vector3(0f, offset.y, 0f);

            Transform.position = Vector3.Lerp(Transform.position, finalPosition, moveSpeed * Time.deltaTime);
            await UniTask.Yield();
        }
    }

    private void ValidateTask()
    {
        if (_task.Status.IsCompleted() || _task.Status.IsCanceled() || _cancellationTokenSource == null)
            return;

        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
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