public class FigureCompositionBinder
{
    public void Bind(FigureCompositionView view, FigureCompositionPresenter presenter)
    {
        view.Initialize();
        presenter.Subscribe();
    }
}
