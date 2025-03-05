using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class ActivatableUI : Activatable
{
    [SerializeField] private TransformView _transformView;
    [SerializeField] private float _appearDuration;

    private void Awake()
    {
        _transformView.Initialize();
    }

    public override void Activate()
    {
        transform.gameObject.SetActive(true); //сделать метод инит в котором будет кэшироваться трансформ, тк объект выключен изначально

        Time.timeScale = 0.5f;

        LMotion.Create(Vector3.zero, _transformView.Transform.localScale, _appearDuration)
            .WithEase(Ease.OutElastic)
            .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
            .BindToLocalScale(_transformView.Transform);
    }

    public override void Deactivate()
    {
        LMotion.Create(_transformView.Transform.localScale, Vector3.zero, _appearDuration)
            .WithEase(Ease.InElastic)
            .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
            .WithOnComplete(FinalizeDeactivation)
            .BindToLocalScale(_transformView.Transform);
    }

    private void FinalizeDeactivation()
    {
        Time.timeScale = 1f;
        transform.gameObject.SetActive(false);
        _transformView.SetStartTransform();
    }
}