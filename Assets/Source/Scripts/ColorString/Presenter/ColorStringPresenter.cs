using System;
using UnityEngine;

public class ColorStringPresenter : IDisposable
{
    private readonly ColorString _model;
    private readonly ColorStringView _view;

    public ColorStringPresenter(ColorString model, ColorStringView view)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        if (view == null)
            throw new ArgumentNullException(nameof(view));

        _model = model;
        _view = view;

        _model.EnableStateSwitched += OnStateSwitched;
    }

    public void Dispose()
    {
        _model.EnableStateSwitched -= OnStateSwitched;
    }

    public Color GetColor() => _model.Color;

    private void OnStateSwitched()
    {
        if (_model.IsEnabled)
            _view.Appear();
        else
            _view.Disappear();
    }
}
