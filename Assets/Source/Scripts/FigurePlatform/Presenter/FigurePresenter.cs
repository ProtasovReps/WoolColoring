using System;
using UnityEngine;

public class FigurePresenter : IEventListener
{
    private readonly Figure _model;
    private readonly FigureView _view;

    public FigurePresenter(Figure model, FigureView view)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        if (view == null)
            throw new ArgumentNullException(nameof(view));

        _model = model;
        _view = view;
    }

    public void Subscribe()
    {
        _model.Appeared += OnAppeared;
        _model.ColorChanged += OnColorChanged;
    }

    public void Unsubscribe()
    {
        _model.Appeared -= OnAppeared;
        _model.ColorChanged -= OnColorChanged;
    }

    public void Fall()
    {
        _model.Fall();
    }

    private void OnAppeared()
    {
        _view.Appear();
    }

    private void OnColorChanged(Color color)
    {
        _view.SetColor(color);
    }
}