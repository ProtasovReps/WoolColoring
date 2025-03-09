using LitMotion;
using LitMotion.Extensions;
using System;
using UnityEngine;

public class StringHolderAnimations : MonoBehaviour
{
    [SerializeField] private float _appearDuration = 1.5f;
    [SerializeField] private float _shakeDuration = 0.4f;
    [SerializeField] private float _jumpDuration = 0.4f;
    [SerializeField] private float _switchDuration = 0.5f;

    public void Appear(Transform transform)
    {
        Vector3 startScale = transform.localScale;

        LMotion.Create(Vector3.zero, startScale, _appearDuration)
            .WithEase(Ease.InOutElastic)
            .BindToLocalScale(transform);
    }

    public void Shake(Transform transform, Vector3 startScale)
    {
        Vector3 targetScale = transform.localScale * 0.95f;

        LSequence.Create()
            .Append(LMotion.Create(transform.localScale, targetScale, _shakeDuration)
            .WithEase(Ease.InElastic)
            .BindToLocalScale(transform))
            .Append(LMotion.Create(targetScale, startScale, _shakeDuration)
            .WithEase(Ease.OutElastic)
            .BindToLocalScale(transform))
            .Run();
    }

    public void Jump(Transform transform, Action callback)
    {
        Vector3 position = transform.position;
        Vector3 scale = transform.localScale;
        Vector3 targetScale = scale * 0.75f;
        Vector3 rotation = transform.localRotation.eulerAngles;
        Vector3 targetRotation = new(rotation.x - 360f, rotation.y, rotation.z);
        float targetUpPosition = position.y + (Vector3.up * 0.5f).y;
        float fallInterval = 0.2f;
        float saltoDuration = _jumpDuration * 2f;

        LSequence.Create()
            .Append(LMotion.Create(position.y, targetUpPosition, _jumpDuration)
                .WithEase(Ease.InQuint)
                .BindToPositionY(transform))
            .Join(LMotion.Create(scale, targetScale, _jumpDuration)
                .WithEase(Ease.InElastic)
                .BindToLocalScale(transform))
            .Join(LMotion.Create(rotation, targetRotation, saltoDuration)
                .WithEase(Ease.InOutExpo)
                .BindToLocalEulerAngles(transform))
            .AppendInterval(fallInterval)
            .Append(LMotion.Create(targetScale, scale, _jumpDuration)
                .WithEase(Ease.InOutElastic)
                .BindToLocalScale(transform))
            .Join(LMotion.Create(targetUpPosition, position.y, _jumpDuration)
                .WithEase(Ease.OutQuint)
                .WithOnComplete(callback)
                .BindToPositionY(transform))
            .Run();
    }

    public void Slide(Transform transform, Transform targetSwitchPosition, Action onTargetPositionReachedCallback, Action onCompleteCallback)
    {
        Vector3 startPosition = transform.position;

        LSequence.Create()
           .Append(LMotion.Create(transform.position.x, targetSwitchPosition.position.x, _switchDuration)
           .WithEase(Ease.InQuint)
           .WithOnComplete(onTargetPositionReachedCallback)
           .BindToPositionX(transform))
           .Append(LMotion.Create(targetSwitchPosition.position.x, startPosition.x, _switchDuration)
           .WithEase(Ease.InOutQuint)
           .WithOnComplete(onCompleteCallback)
           .BindToPositionX(transform))
           .Run();
    }
}