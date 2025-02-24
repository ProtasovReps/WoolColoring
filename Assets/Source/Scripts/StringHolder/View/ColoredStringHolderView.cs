using LitMotion;
using LitMotion.Extensions;
using System;
using UnityEngine;

[RequireComponent(typeof(ColorView))]
public class ColoredStringHolderView : StringHolderView, IColorable
{
    [SerializeField] private Transform _targetSwitchPosition;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _switchDuration;
    [SerializeField] private float _slideDelay;

    private ColorView _colorView;
    private Color _lastColor;
    private ColoredStringHolderPresenter _presenter;

    public Color Color => _presenter.GetColor();

    public void Initialize(ColoredStringHolderPresenter presenter)
    {
        if (presenter == null)
            throw new ArgumentNullException(nameof(presenter));

        _colorView = GetComponent<ColorView>();
        _presenter = presenter;

        _colorView.Initialize();
        Initialize();
    }

    public void Switch()
    {
        if (_lastColor == _presenter.GetColor())
            Jump();
        else
            Slide();
    }

    private void Jump()
    {
        Vector3 position = Transform.position;
        Vector3 scale = Transform.localScale;
        Vector3 targetScale = scale * 0.75f;
        Vector3 rotation = Transform.localRotation.eulerAngles;
        Vector3 targetRotation = new(rotation.x, rotation.y, rotation.z - 360f);
        float targetUpPosition = position.y + (Vector3.up * 0.5f).y;
        float fallInterval = 0.2f;
        float saltoDuration = _jumpDuration * 2f;

        LSequence.Create()
            .Append(LMotion.Create(position.y, targetUpPosition, _jumpDuration)
            .WithEase(Ease.InQuint)
            .BindToPositionY(Transform))
            .Join(LMotion.Create(scale, targetScale, _jumpDuration)
            .WithEase(Ease.InElastic)
            .BindToLocalScale(Transform))
            .Join(LMotion.Create(rotation, targetRotation, saltoDuration)
            .WithEase(Ease.InOutExpo)
            .BindToLocalEulerAngles(Transform))
            .AppendInterval(fallInterval)
            .Append(LMotion.Create(targetScale, scale, _jumpDuration)
            .WithEase(Ease.InOutElastic)
            .BindToLocalScale(Transform))
            .Join(LMotion.Create(targetUpPosition, position.y, _jumpDuration)
            .WithEase(Ease.OutQuint)
            .BindToPositionY(Transform))
            .Run();
    }

    private void Slide()
    {
        LSequence.Create()
            .AppendInterval(_slideDelay)
            .Append(LMotion.Create(Transform.position.x, _targetSwitchPosition.position.x, _switchDuration)
            .WithEase(Ease.InQuint)
            .WithOnComplete(SetColor)
            .BindToPositionX(Transform))
            .Append(LMotion.Create(_targetSwitchPosition.position.x, TransformView.StartPosition.x, _switchDuration)
            .WithEase(Ease.InOutQuint)
            .BindToPositionX(Transform))
            .Run();
    }

    private void SetColor()
    {
        Color color = _presenter.GetColor();

        _lastColor = color;

        _colorView.SetColor(color);
    }
}