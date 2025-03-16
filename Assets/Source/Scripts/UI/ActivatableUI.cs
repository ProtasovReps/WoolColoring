using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class ActivatableUI : Activatable
{
    [SerializeField] private TransformView _transformToAnimate;
    [SerializeField] private float _appearDuration;

    private Transform _transform;

    public void Initialize()
    {
        _transformToAnimate.Initialize();
        _transform = transform;
    }

    public override void Activate()
    {
        _transform.gameObject.SetActive(true);

        LMotion.Create(Vector3.zero, _transformToAnimate.Transform.localScale, _appearDuration)
            .WithEase(Ease.OutElastic)
            .BindToLocalScale(_transformToAnimate.Transform);
    }

    public override void Deactivate()
    {
        LMotion.Create(_transformToAnimate.Transform.localScale, Vector3.zero, _appearDuration)
            .WithEase(Ease.InElastic)
            .WithOnComplete(FinalizeDeactivation)
            .BindToLocalScale(_transformToAnimate.Transform);
    }

    private void FinalizeDeactivation()
    {
        _transform.gameObject.SetActive(false);
        _transformToAnimate.SetStartTransform();
    }
}