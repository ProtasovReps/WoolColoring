public class ColorStringFactory
{
    public ColorString Produce(ColorStringView view)
    {
        var model = new ColorString();
        var presenter = new ColorStringPresenter(model, view);

        view.Initialize(presenter);
        return model;
    }
}