using System;

public class ExplodeFigureStrategy : IBuff
{
    private readonly FigureClickReader _figureClickReader;

    public ExplodeFigureStrategy(FigureClickReader figureClickReader)
    {
        if(figureClickReader == null)
            throw new ArgumentNullException(nameof(figureClickReader));

        _figureClickReader = figureClickReader;
    }

    public int Price => BuffPrices.ExplodeFigurePrice;

    public void Execute() => _figureClickReader.SetPause(false);

    public bool Validate() => _figureClickReader.IsPaused == true;
}