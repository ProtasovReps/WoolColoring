using System;
using UnityEngine;

public class ColoredStringHolderPresenter : IDisposable
{
    private readonly ColoredStringHolder _model;
    private readonly ColoredStringHolderView _view;

    public ColoredStringHolderPresenter(ColoredStringHolderView view, ColoredStringHolder model)
    {
        if (view == null)
            throw new ArgumentNullException(nameof(view));

        if (model == null)
            throw new ArgumentNullException(nameof(model));

        _view = view;
        _model = model;

        _model.StringAdded += OnStringAdded;
        _model.ColorChanged += OnColorChanged;
    }

    public Color GetColor() => _model.Color;

    public void Dispose()
    {
        _model.StringAdded -= OnStringAdded;
        _model.ColorChanged -= OnColorChanged;
    }

    private void OnColorChanged() => _view.Switch();

    private void OnStringAdded() => _view.Shake();
}