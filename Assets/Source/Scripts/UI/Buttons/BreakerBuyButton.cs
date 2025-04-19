using Reflex.Attributes;

public class BreakerBuyButton : BuyBuffButton
{
    [Inject]
    private void Inject(Breaker breaker)
    {
        base.Initialize(breaker);
    }
}