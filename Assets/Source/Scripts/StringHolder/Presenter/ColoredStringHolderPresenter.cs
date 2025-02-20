using System;

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

        _model.ColorChanged += OnColorChanged;
    }

    public void Dispose() => _model.ColorChanged -= OnColorChanged;

    private void OnColorChanged() => _view.SetColor(_model.Color);
}
