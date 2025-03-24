using System;
using UnityEngine;

public class StringDistributor : IDisposable
{
    private readonly ColoredStringHolderStash _coloredHolderStash;
    private readonly WhiteStringHolder _whiteHolder;
    private readonly ColoredStringHolderSwitcher _switcher;

    public event Action<Bolt> BoltDistributing;
    public event Action<Color, int> WhiteHolderDistributing;

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

        _switcher.HolderSwitched += OnHolderSwitched;
    }

    public void Dispose()
    {
        _switcher.HolderSwitched -= OnHolderSwitched;
    }

    public void Distribute(Bolt bolt)
    {
        if (bolt == null)
            throw new ArgumentNullException(nameof(bolt));

        IColorable colorString = bolt.Colorable;

        BoltDistributing?.Invoke(bolt);

        if (_coloredHolderStash.TryGetColoredStringHolder(colorString.Color, out ColoredStringHolder holder))
            holder.Add(colorString);
        else
            _whiteHolder.Add(colorString);
    }

    private void OnHolderSwitched(ColoredStringHolder holder)
    {
        Color requiredColor = holder.Color;
        int requiredStringCount = _whiteHolder.GetRequiredColorsCount(requiredColor);
        int holderEmptySlotsCount = holder.MaxStringCount - holder.StringCount;

        if (requiredStringCount == 0)
            return;

        requiredStringCount = Mathf.Clamp(requiredStringCount, 0, holderEmptySlotsCount);
        WhiteHolderDistributing?.Invoke(requiredColor, holderEmptySlotsCount);

        for (int i = 0; i < requiredStringCount; i++)
        {
            IColorable colorable = _whiteHolder.GetRequiredColorable(requiredColor);
            holder.Add(colorable);
        }
    }
}
