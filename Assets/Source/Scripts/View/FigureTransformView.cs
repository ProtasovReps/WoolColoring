using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class FigureTransformView : TransformView
{
    private CancellationTokenSource _cancellationTokenSource;
    private float _minDistance = 0.01f;

    public void ChangePosition(Vector3 targetPosition, Collider[] colliders, float moveSpeed)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();

        SetPositionAnimated(targetPosition, colliders, moveSpeed).Forget();
    }

    private async UniTaskVoid SetPositionAnimated(Vector3 position, Collider[] colliders, float moveSpeed)
    {
        _cancellationTokenSource = new CancellationTokenSource();

        SetColliderEnableState(false, colliders);

        while (GetSquareMagnitude(position, Transform.position) > _minDistance)
        {
            Transform.position = Vector3.Lerp(Transform.position, position, moveSpeed * Time.deltaTime);
            await UniTask.Yield(PlayerLoopTiming.Update, _cancellationTokenSource.Token);
        }

        SetColliderEnableState(true, colliders);
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
}