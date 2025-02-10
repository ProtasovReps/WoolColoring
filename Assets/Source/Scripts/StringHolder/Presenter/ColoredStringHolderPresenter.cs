using System;

public class ColoredStringHolderPresenter : StringHolderPresenter
{
    private readonly ColoredStringHolderView _view;
    private readonly ColoredStringHolder _model;

    public ColoredStringHolderPresenter(StringHolderView view, StringHolder model) : base(view, model)
    {
        if(view is not ColoredStringHolderView coloredStringView)
            throw new ArgumentException("Colored view required");

        if(model is not ColoredStringHolder coloredStringModel)
            throw new ArgumentException("Colored model required");

        _view = coloredStringView;
        _model = coloredStringModel;
    }

    public override void Subscribe()
    {
        _model.ColorChanged += OnColorChanged;
        base.Subscribe();
    }

    public override void Unsubscribe()
    {
        _model.ColorChanged -= OnColorChanged;
        base.Unsubscribe();
    }

    private void OnColorChanged()
    {
        _view.SetColor(_model.Color);
    }
}
