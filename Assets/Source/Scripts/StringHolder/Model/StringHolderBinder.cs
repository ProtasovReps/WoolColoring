using System.Collections.Generic;

public class StringHolderBinder
{
    private ColorStringFactory _colorStringFactory;

    public StringHolderBinder()
    {
        _colorStringFactory = new ColorStringFactory();
    }

    public ColoredStringHolder Bind(ColoredStringHolderView view)
    {
        var strings = GetColorStrings(view);
        var model = new ColoredStringHolder(strings);
        var presenter = new ColoredStringHolderPresenter(view, model);

        view.Initialize(presenter);
        return model;
    }

    public WhiteStringHolder Bind(WhiteStringHolderView view, Picture picture)
    {
        var strings = GetColorStrings(view);
        var model = new WhiteStringHolder(strings);
        var stringRemover = new ExtraStringRemover(picture, model);
        var presenter = new WhiteStringHolderPresenter(view, model);

        view.Initialize();
        return model;
    }

    private ColorString[] GetColorStrings(StringHolderView view)
    {
        var tempList = new List<ColorString>();
        ColorString newColorString = null;

        foreach (ColorStringView colorString in view.Strings)
        {
            newColorString = _colorStringFactory.Produce(colorString);
            tempList.Add(newColorString);
        }

        return tempList.ToArray();
    }
}
