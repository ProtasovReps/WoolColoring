using System;

public class Breaker : IBuff
{
    private readonly FigureClickReader _figureClickReader;

    public Breaker(FigureClickReader figureClickReader)
    {
        if(figureClickReader == null)
            throw new ArgumentNullException(nameof(figureClickReader));

        _figureClickReader = figureClickReader;
    }

    public int Price => BuffPrices.ExplodeFigurePrice;

    public string Id => RewardIds.Breaker;

    public void Execute() => _figureClickReader.SetPause(false);

    public bool Validate() => _figureClickReader.IsPaused == true;
}