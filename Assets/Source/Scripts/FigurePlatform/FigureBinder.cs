public class FigureBinder
{
    public Figure Bind(Figure model, FigureView view, FigurePresenter presenter)
    {
        view.Initialize(presenter);
        return model;
    }
}
