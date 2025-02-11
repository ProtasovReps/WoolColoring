using System;
using UnityEngine;

public class Painter : IUnsubscribable
{
    private readonly Picture _picture;
    private readonly ColoredStringHolderStash _holderStash;
    private readonly ColoredStringHolderSwitcher _switcher;
    private int _blocksPerHolder;

    public Painter(Picture picture, ColoredStringHolderSwitcher switcher, ColoredStringHolderStash stash, int blocksPerHolder)
    {
        if (picture == null)
            throw new NullReferenceException(nameof(_holderStash));

        if (switcher == null)
            throw new NullReferenceException(nameof(switcher));

        if (stash == null)
            throw new NullReferenceException(nameof(stash));

        if (blocksPerHolder <= 0)
            throw new ArgumentException(nameof(blocksPerHolder));

        _picture = picture;
        _holderStash = stash;
        _switcher = switcher;
        _blocksPerHolder = blocksPerHolder;

        Subscribe();
    }

    public void Unsubscribe()
    {
        foreach (IFillable<StringHolder> holder in _holderStash.ColoredStringHolders)
            holder.Filled -= OnFilled;
    }

    private void Subscribe()
    {
        foreach (IFillable<StringHolder> holder in _holderStash.ColoredStringHolders)
            holder.Filled += OnFilled;
    }

    private void OnFilled(StringHolder holder)
    {
        if (holder is ColoredStringHolder coloderHolder == false)
            throw new InvalidCastException();

        FillImage(coloderHolder);
    }

    private void FillImage(ColoredStringHolder holder)
    {
        Color color = Color.black;

        for (int i = 0; i < holder.MaxStringCount; i++)
            color = holder.GetColorable().Color;

        for (int i = 0; i < _blocksPerHolder; i++)
            _picture.Colorize(color);

        _switcher.ChangeStringHolderColor(holder);
    }
}