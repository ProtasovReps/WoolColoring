using Reflex.Attributes;

public class UnlockerBuyButton : BuyBuffButton
{
    [Inject]
    private void Inject(Unlocker unlocker)
    {
        base.Initialize(unlocker);
    }
}