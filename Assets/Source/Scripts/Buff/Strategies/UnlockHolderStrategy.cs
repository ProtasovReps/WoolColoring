using System;

public class UnlockHolderStrategy : IBuff
{
    private readonly ColoredStringHolderStash _coloredStash;
    private readonly ColoredStringHolderSwitcher _coloredStringHolderSwitcher;
    private readonly Picture _picture;

    public UnlockHolderStrategy(ColoredStringHolderStash stash, ColoredStringHolderSwitcher switcher, Picture picture, int price)
    {
        if (stash == null)
            throw new ArgumentNullException(nameof(stash));

        if (switcher == null)
            throw new ArgumentNullException(nameof(switcher));

        _coloredStringHolderSwitcher = switcher;
        _coloredStash = stash;
        Price = price;
    }

    public int Price { get; }

    public bool Validate() => _picture.RequiredColorsCount != _coloredStash.ActiveCount;

    public void Execute()
    {
        if (Validate() == false)
            throw new InvalidOperationException(nameof(Validate));

        ColoredStringHolder unlockedHolder = _coloredStash.UnlockHolder();

        _coloredStringHolderSwitcher.Switch(unlockedHolder);
    }
}