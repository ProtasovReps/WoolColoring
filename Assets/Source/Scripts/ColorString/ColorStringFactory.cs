public class ColorStringFactory
{
    public ColorString Produce(ColorStringView view, ObjectDisposer disposer)
    {
        var model = new ColorString();
        var presenter = new ColorStringPresenter(model, view);

        disposer.Add(presenter);
        view.Initialize(presenter);
        return model;
    }
}