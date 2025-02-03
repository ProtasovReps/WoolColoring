using System;
using UnityEngine;

public class Painter : IDisposable
{
    private readonly Picture _picture;
    private readonly ColoredStringHolderStash _holderStash;
    private readonly ColoredStringHolderSwitcher _switcher;

    public Painter(Picture picture, ColoredStringHolderSwitcher switcher, ColoredStringHolderStash stash)
    {
        if (picture == null)
            throw new NullReferenceException(nameof(_holderStash));

        if (switcher == null)
            throw new NullReferenceException(nameof(switcher));

        if (stash == null)
            throw new NullReferenceException(nameof(stash));

        _picture = picture;
        _holderStash = stash;
        _switcher = switcher;

        Subscribe();
    }

    public void Subscribe()
    {
        foreach (IFillable<StringHolder> holder in _holderStash.ColoredStringHolders)
            holder.Filled += OnFilled;
    }

    public void Dispose()
    {
        foreach (IFillable<StringHolder> holder in _holderStash.ColoredStringHolders)
            holder.Filled -= OnFilled;
    }

    private void OnFilled(StringHolder holder)
    {
        if (holder is ColoredStringHolder coloderHolder == false)
            throw new InvalidCastException();

        FillImage(coloderHolder);
    }

    private void FillImage(ColoredStringHolder holder)
    {
        for (int i = 0; i < holder.MaxStringCount; i++)
        {
            Color color = holder.GetColorable().Color;

            _picture.Colorize(color);
        }

        Color requiredColor = _picture.GetRequiredColor();

        _switcher.Switch(requiredColor, holder);
    }
}