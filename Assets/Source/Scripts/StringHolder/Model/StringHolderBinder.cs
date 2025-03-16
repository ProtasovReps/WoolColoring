using System.Collections.Generic;

public class StringHolderBinder
{
    private readonly ColorStringFactory _colorStringFactory;
    private readonly ColoredStringHolderView[] _coloredViews;
    private readonly WhiteStringHolderView _whiteView;

    public StringHolderBinder(ColorStringFactory colorStringFactory, ColoredStringHolderView[] coloredViews, WhiteStringHolderView whiteView)
    {
        _colorStringFactory = colorStringFactory;
        _coloredViews = coloredViews;
        _whiteView = whiteView;
    }

    public ColoredStringHolder[] BindColoredHolders(StringHolderAnimations animations, HolderSoundPlayer soundPlayer)
    {
        var holderModels = new ColoredStringHolder[_coloredViews.Length];
        ColorString[] strings;
        ColoredStringHolder model;
        ColoredStringHolderPresenter presenter;
        ColoredStringHolderView view;

        for (int i = 0; i < _coloredViews.Length; i++)
        {
            view = _coloredViews[i];
            strings = GetColorStrings(view);
            model = new ColoredStringHolder(strings);
            presenter = new ColoredStringHolderPresenter(view, model);
            view.Initialize(presenter, animations, soundPlayer);

            holderModels[i] = model;
        }

        return holderModels;
    }

    public WhiteStringHolder BindWhiteHolder(StringHolderAnimations animations)
    {
        ColorString[] strings = GetColorStrings(_whiteView);
        WhiteStringHolder model = new(strings);
        WhiteStringHolderPresenter presenter = new(model, _whiteView);

        _whiteView.Initialize(animations);
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
