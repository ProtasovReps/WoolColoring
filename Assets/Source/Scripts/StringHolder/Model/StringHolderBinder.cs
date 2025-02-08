using System.Collections.Generic;

public class StringHolderBinder
{
    public ColoredStringHolder Bind(ColoredStringHolderView view)
    {
        var strings = GetColorStrings(view);
        var model = new ColoredStringHolder(strings);
        var presenter = new ColoredStringHolderPresenter(view, model);

        Prepare(view, presenter);
        return model;
    }

    public WhiteStringHolder Bind(StringHolderView view)
    {
        var strings = GetColorStrings(view);
        var model = new WhiteStringHolder(strings);
        var presenter = new StringHolderPresenter(view, model);

        Prepare(view, presenter);
        return model;
    }

    private ColorString[] GetColorStrings(StringHolderView view)
    {
        var tempList = new List<ColorString>();

        foreach (ColorString colorString in view.Strings)
            tempList.Add(colorString);

        return tempList.ToArray();
    }

    private void Prepare(StringHolderView view, StringHolderPresenter presenter)
    {
        view.Initialize(presenter);
        presenter.Subscribe();
    }
}
