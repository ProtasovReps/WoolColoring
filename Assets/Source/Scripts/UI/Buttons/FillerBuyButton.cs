using Reflex.Attributes;

public class FillerBuyButton : BuyBuffButton
{
    [Inject]
    private void Inject(Filler filler)
    {
        base.Initialize(filler);
    }
}