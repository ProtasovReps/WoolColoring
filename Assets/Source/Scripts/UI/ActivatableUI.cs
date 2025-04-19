using Reflex.Attributes;
using UnityEngine;

public class ActivatableUI : Activatable
{
    [SerializeField] private TransformView _transformToAnimate;
    [SerializeField] private float _animationDuration;

    private Transform _transform;
    private UIAnimations _animator;

    protected UIAnimations Animator => _animator;
    protected bool IsAnimating { get; private set; }


    [Inject]
    private void Inject(UIAnimations animator) => _animator = animator;

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
    {
        IsAnimating = true;
        _animator.PopOut(_transformToAnimate.Transform, _animationDuration, FinalizeDeactivation);
    }

    private void FinalizeDeactivation()
    {
        IsAnimating = false;
        _transform.gameObject.SetActive(false);
        _transformToAnimate.gameObject.SetActive(false);
        _transformToAnimate.Transform.localScale = _transformToAnimate.StartScale;
    }
}