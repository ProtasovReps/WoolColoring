using System;

public class StringDistributor
{
    private ColoredStringHolderStash _coloredHolderStash;
    private WhiteStringHolder _whiteHolder;

    public StringDistributor(ColoredStringHolderStash stash, WhiteStringHolder whiteHolder)
    {
        if (stash == null)
            throw new ArgumentNullException(nameof(stash));

        if(whiteHolder == null)
            throw new ArgumentNullException(nameof(whiteHolder));

        _coloredHolderStash = stash;
        _whiteHolder = whiteHolder;
    }

    public void Distribute(StringBolt bolt)
    {
        if (bolt == null)
            throw new ArgumentNullException(nameof(bolt));

        IColorable colorString = bolt.ColorString;

        if (_coloredHolderStash.TryGetColoredStringHolder(colorString.GetColor(), out ColoredStringHolder holder))
            holder.Add(colorString);
        else
            _whiteHolder.Add(colorString);
    }
}
