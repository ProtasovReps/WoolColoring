using LitMotion;
using LitMotion.Extensions;
using System;
using UnityEngine;

public class BoltAnimations : MonoBehaviour
{
    private const float FullRotation = 360f;

    [SerializeField] private float _unscrewDuration = 0.2f;
    [SerializeField] private int _unscrewLoopCount = 3;
    [SerializeField] private float _targetScale = 0.5f;
    [SerializeField] private float _upPosition = 0.5f;

    public void Unscrew(Transform transform, Action callback)
    {
        Vector3 rotation = transform.localRotation.eulerAngles;
        float targetRotation = rotation.y - FullRotation;
        Vector3 targetPosition = transform.position + transform.up * _upPosition;
        Vector3 targetScale = transform.localScale * _targetScale;

        LSequence.Create()
            .Join(LMotion.Create(rotation, new Vector3(rotation.x, targetRotation, rotation.z), _unscrewDuration)
                .WithLoops(_unscrewLoopCount, LoopType.Incremental)
                .WithOnComplete(callback)
                .BindToLocalEulerAngles(transform))
            .Join(LMotion.Create(transform.position, targetPosition, _unscrewDuration)
                .WithEase(Ease.InOutQuint)
                .BindToPosition(transform))
            .Join(LMotion.Create(transform.localScale, targetScale, _unscrewDuration)
                .WithLoops(_unscrewLoopCount, LoopType.Yoyo)
                .BindToLocalScale(transform))
            .Run();
    }
}