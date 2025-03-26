using LitMotion;
using LitMotion.Extensions;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UIAnimator : MonoBehaviour
{
    public void PopUp(Transform transform, float duration)
    {
        LMotion.Create(Vector3.zero, transform.localScale, duration)
            .WithEase(Ease.OutElastic)
            .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
            .BindToLocalScale(transform);
    }

    public void PopOut(Transform transform, float duration, Action callback)
    {
        LMotion.Create(transform.localScale, Vector3.zero, duration)
           .WithEase(Ease.InElastic)
           .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
           .WithOnComplete(callback)
           .BindToLocalScale(transform);
    }

    public void FadeAlpha(Image image, float targetAlpha, float duration)
    {
        LMotion.Create(0f, targetAlpha, duration)
            .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
            .BindToColorA(image);
    }
}