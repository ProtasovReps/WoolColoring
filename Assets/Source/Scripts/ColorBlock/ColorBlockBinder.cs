
public class ColorBlockBinder
{
    public ColorBlock Bind(ColorBlockView view)
    {
        var model = new ColorBlock(view.RequiredColor);
        var presenter = new ColorBlockPresenter(view, model);

        presenter.Subscribe();
        return model;
    }
}
