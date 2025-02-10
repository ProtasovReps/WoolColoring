public class ColoredStringHolderPresenter : StringHolderPresenter
{
    public ColoredStringHolderPresenter(ColoredStringHolderView view, ColoredStringHolder model) : base(view, model) { }

    public override void Subscribe()
    {
        ColoredStringHolder model = Model as ColoredStringHolder;

        model.ColorChanged += OnColorChanged;
        base.Subscribe();
    }

    public override void Unsubscribe()
    {
        ColoredStringHolder model = Model as ColoredStringHolder;

        model.ColorChanged -= OnColorChanged;
        base.Unsubscribe();
    }

    private void OnColorChanged()
    {
        ColoredStringHolder model = Model as ColoredStringHolder;
        ColoredStringHolderView view = View as ColoredStringHolderView;

        view.SetColor(model.Color);
    }
}
