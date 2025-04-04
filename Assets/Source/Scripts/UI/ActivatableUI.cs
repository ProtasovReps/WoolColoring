using Reflex.Attributes;
using UnityEngine;

public class ActivatableUI : Activatable
{
    [SerializeField] private TransformView _transformToAnimate;
    [SerializeField] private float _animationDuration;

    private Transform _transform;
    private UIAnimator _animator;

    protected UIAnimator Animator => _animator;

    public virtual void Initialize()
    {
        _transformToAnimate.Initialize();
        _transform = transform;
    }

    public override void Activate()
    {
        _transform.gameObject.SetActive(true);
        _transformToAnimate.gameObject.SetActive(true);

        _animator.PopUp(_transformToAnimate.Transform, _animationDuration);
    }

    public override void Deactivate()
        => _animator.PopOut(_transformToAnimate.Transform, _animationDuration, FinalizeDeactivation);

    private void FinalizeDeactivation()
    {
        _transform.gameObject.SetActive(false);
        _transformToAnimate.gameObject.SetActive(false);
        _transformToAnimate.Transform.localScale = _transformToAnimate.StartScale;
    }

    [Inject]
    private void Inject(UIAnimator animator) => _animator = animator;
}