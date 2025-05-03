using System;

public class Remover : IBuff
{
    private WhiteStringHolder _whiteHolder;

    public Remover(WhiteStringHolder whiteHolder)
    {
        if(whiteHolder == null)
            throw new ArgumentNullException(nameof(whiteHolder));

        _whiteHolder = whiteHolder;
    }

    public int Price => BuffPrices.ClearWhiteHolderPrice;
    public string Id => RewardIds.Remover;

    public void Execute() => _whiteHolder.RemoveAllStrings();

    public bool Validate() => _whiteHolder.StringCount > 0;
}