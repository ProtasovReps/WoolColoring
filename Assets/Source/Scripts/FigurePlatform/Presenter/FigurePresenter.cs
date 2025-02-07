using System;

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
