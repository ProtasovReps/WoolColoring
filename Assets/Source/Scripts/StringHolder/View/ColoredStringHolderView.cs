using LitMotion;
using LitMotion.Extensions;
using System;
using UnityEngine;

[RequireComponent(typeof(TransformView))]
[RequireComponent(typeof(ColorView))]
public class ColoredStringHolderView : StringHolderView
{
    [SerializeField] private Transform _targetSwitchPosition;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _switchDuration;

    private ColorView _colorView;
    private Color _lastColor;
    private TransformView _transformView;
    private ColoredStringHolderPresenter _presenter;

    public void Initialize(ColoredStringHolderPresenter presenter)
    {
        if (presenter == null)
            throw new ArgumentNullException(nameof(presenter));

        _colorView = GetComponent<ColorView>();
        _transformView = GetComponent<TransformView>();
        _presenter = presenter;

        _transformView.Initialize();
        _colorView.Initialize();
    }

    public void Switch()
    {
        if (_presenter.GetColor() == _lastColor)
            Jump();
        else
            Slide();
    }

    private void Jump()
    {
        Vector3 position = _transformView.Transform.position;
        Vector3 scale = _transformView.Transform.localScale;
        Vector3 targetScale = scale * 1.25f;
        float targetUpPosition = position.y + (Vector3.up * 0.5f).y;
        float fallInterval = 0.2f;

        LSequence.Create()
            .Append(LMotion.Create(position.y, targetUpPosition, _jumpDuration)
            .WithEase(Ease.InQuint)
            .BindToPositionY(_transformView.Transform))
            .Join(LMotion.Create(scale, targetScale, _jumpDuration)
            .WithEase(Ease.InElastic)
            .BindToLocalScale(_transformView.Transform))
            .AppendInterval(fallInterval)
            .Append(LMotion.Create(targetScale, scale, _jumpDuration)
            .WithEase(Ease.InOutElastic)
            .BindToLocalScale(_transformView.Transform))
            .Join(LMotion.Create(targetUpPosition, position.y, _jumpDuration)
            .WithEase(Ease.OutQuint)
            .BindToPositionY(_transformView.Transform))
            .Run();
    }

    private void Slide()
    {
        LSequence.Create()
            .Append(LMotion.Create(_transformView.Transform.position.x, _targetSwitchPosition.position.x, _switchDuration)
            .WithEase(Ease.InQuint)
            .WithOnComplete(SetColor)
            .BindToPositionX(_transformView.Transform))
            .Append(LMotion.Create(_targetSwitchPosition.position.x, _transformView.StartPosition.x, _switchDuration)
            .WithEase(Ease.InOutQuint)
            .BindToPositionX(_transformView.Transform))
            .Run();
    }

    private void SetColor()
    {
        Color color = _presenter.GetColor();

        _lastColor = color;

        _colorView.SetColor(color);
    }
}