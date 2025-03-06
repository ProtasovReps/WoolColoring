using System;
using UnityEngine;

[RequireComponent(typeof(ColorView))]
public class ColoredStringHolderView : StringHolderView, IColorable
{
    [SerializeField] private Transform _targetSwitchPosition;

    private ColorView _colorView;
    private Color _lastColor;
    private ColoredStringHolderPresenter _presenter;
    private ActionQueue _actionQueue;

    public bool IsAnimating { get; private set; }
    public Color Color => _presenter.GetColor();

    public void Initialize(ColoredStringHolderPresenter presenter)
    {
        if (presenter == null)
            throw new ArgumentNullException(nameof(presenter));

        _presenter = presenter;
        _actionQueue = new ActionQueue();
        _colorView = GetComponent<ColorView>();

        _colorView.Initialize();
        Initialize();
    }

    public void Switch()
    {
        IsAnimating = true;

        if (_lastColor == _presenter.GetColor())
            _actionQueue.AddAction(Jump);
        else
            _actionQueue.AddAction(Slide);

        _actionQueue.ValidateAction();
    }

    private void Jump()
    {
        Animations.Jump(Transform, FinalizeAnimation);
    }

    private void Slide()
    {
        Animations.Slide(Transform, _targetSwitchPosition, SetColor, FinalizeAnimation);
    }

    private void FinalizeAnimation()
    {
        IsAnimating = false;
        Transform.rotation = TransformView.StartRotation;
        _actionQueue.ProcessQueuedAction();
    }

    private void SetColor()
    {
        Color color = _presenter.GetColor();

        _lastColor = color;

        _colorView.SetColor(color);
    }
}