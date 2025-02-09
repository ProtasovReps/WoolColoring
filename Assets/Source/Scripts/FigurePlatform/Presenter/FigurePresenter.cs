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
        _model.Appeared += OnAppeared;
        _model.PositionChanged += OnPositionChanged;
    }

    public void Unsubscribe()
    {
        _model.Appeared -= OnAppeared;
        _model.PositionChanged -= OnPositionChanged;
    }

    public void Fall()
    {
        _model.Fall();
    }

    private void OnAppeared()
    {
        Color newColor = _colorizer.GetRandomColor();

        _view.Appear();
        _view.SetColor(newColor);
    }

    private void OnPositionChanged()
    {
        _view.ChangePosition(_model.Position);
    }
}
