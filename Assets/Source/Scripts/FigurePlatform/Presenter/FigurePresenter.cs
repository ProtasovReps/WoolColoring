using System;
using UnityEngine;

public class FigurePresenter : IEventListener
{
    private readonly Figure _model;
    private readonly FigureView _view;
    private readonly ColorPallete _colorizer;

    public FigurePresenter(Figure model, FigureView view, ColorPallete colorizer)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        if (view == null)
            throw new ArgumentNullException(nameof(view));

        if (colorizer == null)
            throw new ArgumentNullException(nameof(colorizer));

        _model = model;
        _view = view;
        _colorizer = colorizer;
    }

    public void Subscribe()
    {
        _model.PositionChanged += OnPositionChanged;
        _model.Appeared += OnAppeared;
    }

    public void Unsubscribe()
    {
        _model.PositionChanged -= OnPositionChanged;
        _model.Appeared -= OnAppeared;
    }

    public void OnAppeared()
    {
        Color newColor = _colorizer.GetRandomColor();

        _view.SetColor(newColor);
        _view.Appear();
    }

    public void Fall()
    {
        _model.Fall();
    }

    private void OnPositionChanged()
    {
        _view.ChangePosition(_model.Position);
    }
}
