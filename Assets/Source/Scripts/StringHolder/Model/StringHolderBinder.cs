public class StringHolderBinder
{
    public ColoredStringHolder Bind(ColoredStringHolderView view)
    {
        var model = new ColoredStringHolder();
        var presenter = new ColoredStringHolderPresenter(view, model);

        model.Initialize(view.Strings);
        view.Initialize(presenter);

        presenter.Subscribe();
        return model;
    }

    public WhiteStringHolder Bind(StringHolderView view)
    {
        var model = new WhiteStringHolder();
        var presenter = new StringHolderPresenter(view, model);

        model.Initialize(view.Strings);
        view.Initialize(presenter);

        presenter.Subscribe();
        return model;
    }
}
