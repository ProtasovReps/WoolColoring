using LitMotion;
using LitMotion.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ColorView))]
[RequireComponent(typeof(TransformView))]
[RequireComponent(typeof(ActiveStateSwitcher))]
public class ColorStringView : MonoBehaviour, IColorSettable, IColorable
{
    [SerializeField] private float _appearDuration = 0.2f;
    [SerializeField] private float _disappearDuration = 1f;

    private Queue<Action> _actions;
    private bool _isAnimated;
    private ColorView _colorView;
    private TransformView _transformView;
    private ActiveStateSwitcher _stateSwitcher;

    public Color Color { get; private set; }

    public void Initialize()
    {
        _stateSwitcher = GetComponent<ActiveStateSwitcher>();
        _transformView = GetComponent<TransformView>();
        _colorView = GetComponent<ColorView>();

        _actions = new Queue<Action>();
        _transformView.Initialize();
        _colorView.Initialize();
        _stateSwitcher.Initialize();
    }

    public void SetColor(Color color)
    {
        Color = color;
        _colorView.SetColor(color);
    }

    public void Appear()
    {
        _actions.Enqueue(AppearAnimated);
        ValidateAnimation();
    }

    public void Disappear()
    {
        _actions.Enqueue(DisappearAnimated);
        ValidateAnimation();
    }

    private void AppearAnimated()
    {
        _stateSwitcher.SetActive(true);

        LMotion.Create(_transformView.Transform.localScale, _transformView.StartScale, _appearDuration)
            .WithOnComplete(ProcessQueuedAnimations)
            .BindToLocalScale(_transformView.Transform);
    }

    private void DisappearAnimated()
    {
        LMotion.Create(_transformView.Transform.localScale, Vector3.zero, _disappearDuration)
            .WithOnComplete(FinalizeDisappearing)
            .BindToLocalScale(_transformView.Transform);
    }

    private void ValidateAnimation()
    {
        if (_isAnimated == false)
        {
            _isAnimated = true;
            ProcessQueuedAnimations();
        }
    }

    private void FinalizeDisappearing()
    {
        _stateSwitcher.SetActive(false);
        ProcessQueuedAnimations();
    }

    private void ProcessQueuedAnimations()
    {
        if (_actions.Count == 0)
        {
            _isAnimated = false;
            return;
        }

        _actions.Dequeue()?.Invoke();
    }
}