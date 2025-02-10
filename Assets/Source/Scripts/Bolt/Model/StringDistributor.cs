using System;
using UnityEngine;

public class StringDistributor : IUnsubscribable
{
    private readonly ColoredStringHolderStash _coloredHolderStash;
    private readonly WhiteStringHolder _whiteHolder;
    private readonly ColoredStringHolderSwitcher _switcher;

    public StringDistributor(ColoredStringHolderStash stash, WhiteStringHolder whiteHolder, ColoredStringHolderSwitcher switcher)
    {
        if (stash == null)
            throw new ArgumentNullException(nameof(stash));

        if (whiteHolder == null)
            throw new ArgumentNullException(nameof(whiteHolder));

        if (switcher == null)
            throw new ArgumentNullException(nameof(switcher));

        _coloredHolderStash = stash;
        _whiteHolder = whiteHolder;
        _switcher = switcher;

        Subscribe();
    }

    public void Unsubscribe()
    {
        _switcher.ColorSwitched -= OnColorChanged;
    }

    public void Distribute(BoltView bolt)
    {
        if (bolt == null)
            throw new ArgumentNullException(nameof(bolt));

        IColorable colorString = bolt.Colorable;

        if (_coloredHolderStash.TryGetColoredStringHolder(colorString.Color, out ColoredStringHolder holder))
            holder.Add(colorString);
        else
            _whiteHolder.Add(colorString);
    }

    private void Subscribe()
    {
        _switcher.ColorSwitched += OnColorChanged;
    }

    private void OnColorChanged(ColoredStringHolder holder)
    {
        _whiteHolder.SetRequiredColor(holder.Color);

        int requiredStringCount = _whiteHolder.GetRequiredColorsCount();

        if (requiredStringCount == 0)
            return;

        requiredStringCount = Mathf.Clamp(requiredStringCount, 0, holder.MaxStringCount);

        for (int i = 0; i < requiredStringCount; i++)
        {
            IColorable colorable = _whiteHolder.GetColorable();
            holder.Add(colorable);
        }
    }
}
