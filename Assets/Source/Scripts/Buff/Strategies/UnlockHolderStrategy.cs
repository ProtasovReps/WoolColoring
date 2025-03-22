using System;

public class UnlockHolderStrategy : IBuff
{
    private readonly ColoredStringHolderStash _coloredStash;
    private readonly ColoredStringHolderSwitcher _coloredStringHolderSwitcher;
    private readonly Picture _picture;

    public UnlockHolderStrategy(ColoredStringHolderStash stash, ColoredStringHolderSwitcher switcher, Picture picture)
    {
        if (stash == null)
            throw new ArgumentNullException(nameof(stash));

        if (switcher == null)
            throw new ArgumentNullException(nameof(switcher));

        if (picture == null)
            throw new ArgumentNullException(nameof(picture));

        _coloredStringHolderSwitcher = switcher;
        _coloredStash = stash;
        _picture = picture;
    }

    public int Price => BuffPrices.UnlockHolderPrice;

    public bool Validate() => _picture.RequiredColorsCount > _coloredStash.ActiveCount;

    public void Execute()
    {
        if (Validate() == false)
            throw new InvalidOperationException(nameof(Validate));

        ColoredStringHolder unlockedHolder = _coloredStash.UnlockHolder();

        _coloredStringHolderSwitcher.Switch(unlockedHolder);
    }
}