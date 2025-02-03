using System;
using UnityEngine;

public class ColorBlockPresenter : IDisposable
{
    private readonly ColorBlockView _view;
    private readonly ColorBlock _model;

    public ColorBlockPresenter(ColorBlockView view, ColorBlock model)
    {
        if (view == null)
            throw new NullReferenceException(nameof(view));

        if (model == null)
            throw new NullReferenceException(nameof(model));

        _view = view;
        _model = model;
    }

    public void Subscribe()
    {
        _model.ColorSetted += OnColorSetted;
    }

    public void Dispose() => Unsubscribe();

    private void Unsubscribe()
    {
        _model.ColorSetted -= OnColorSetted;
    }

    private void OnColorSetted(Color color)
    {
        _view.SetRenderColor(color);
    }
}
