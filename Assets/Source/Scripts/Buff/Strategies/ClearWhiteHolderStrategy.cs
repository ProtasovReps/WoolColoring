using System;

public class ClearWhiteHolderStrategy : IBuff
{
    private WhiteStringHolder _whiteHolder;

    public ClearWhiteHolderStrategy(WhiteStringHolder whiteHolder)
    {
        if(whiteHolder == null)
            throw new ArgumentNullException(nameof(whiteHolder));

        _whiteHolder = whiteHolder;
    }

    public int Price => BuffPrices.ClearWhiteHolderPrice;

    public void Execute() => _whiteHolder.RemoveAllStrings();

    public bool Validate() => _whiteHolder.StringCount > 0;
}