using System;
using UnityEngine;

[RequireComponent(typeof(ColorView))]
public class ColoredStringHolderView : StringHolderView, IColorable
{
    [SerializeField] private Transform _targetSwitchPosition;
    [SerializeField] private SpriteRenderer _padlockSprite;

    private ColorView _colorView;
    private Color _lastColor;
    private ColoredStringHolderPresenter _presenter;
    private ActionQueue _actionQueue;
    private HolderSoundPlayer _soundPlayer;

    public bool IsAnimating => _actionQueue.IsAnimating;
    public Color Color => _presenter.GetColor();

    public void Initialize(ColoredStringHolderPresenter presenter, StringHolderAnimations animations, HolderSoundPlayer holderSoundPlayer)
    {
        if (presenter == null)
            throw new ArgumentNullException(nameof(presenter));

        _presenter = presenter;
        _actionQueue = new ActionQueue();
        _colorView = GetComponent<ColorView>();
        _soundPlayer = holderSoundPlayer;

        _colorView.Initialize();
        base.Initialize(animations);
    }

    public void PlayFilledSound() => _soundPlayer.Fill();

    public void Switch()
    {
        if (_lastColor == _presenter.GetColor())
            _actionQueue.AddAction(Jump);
        else
            _actionQueue.AddAction(Slide);

        _actionQueue.ValidateAction();
    }

    private void Jump()
    {
        _soundPlayer.Switch();
        Animations.Jump(Transform, FinalizeAnimation);
    }

    private void Slide()
    {
        _soundPlayer.Switch();
        Animations.Slide(Transform, _targetSwitchPosition, SetColor, FinalizeAnimation);
    }

    private void FinalizeAnimation()
    {
        Transform.rotation = TransformView.StartRotation;
        _actionQueue.ProcessQueuedAction();
    }

    private void SetColor()
    {
        Color color = _presenter.GetColor();

        _padlockSprite.enabled = color == ColorStates.InactiveColor;
        _lastColor = color;

        _colorView.SetColor(color);
    }
}