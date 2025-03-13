using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class ActivatableUI : Activatable
{
    [SerializeField] private TransformView _transformToAnimate;
    [SerializeField] private float _appearDuration;

    private Transform _transform;

    private void Awake()
    {
        _transformToAnimate.Initialize();
        _transform = transform;
        _transform.gameObject.SetActive(false);
    }

    public override void Activate()
    {
        _transform.gameObject.SetActive(true);

        Time.timeScale = 0.5f;

        LMotion.Create(Vector3.zero, _transformToAnimate.Transform.localScale, _appearDuration)
            .WithEase(Ease.OutElastic)
            .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
            .BindToLocalScale(_transformToAnimate.Transform);
    }

    public override void Deactivate()
    {
        LMotion.Create(_transformToAnimate.Transform.localScale, Vector3.zero, _appearDuration)
            .WithEase(Ease.InElastic)
            .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
            .WithOnComplete(FinalizeDeactivation)
            .BindToLocalScale(_transformToAnimate.Transform);
    }

    private void FinalizeDeactivation()
    {
        Time.timeScale = 1f;
        _transform.gameObject.SetActive(false);
        _transformToAnimate.SetStartTransform();
    }
}