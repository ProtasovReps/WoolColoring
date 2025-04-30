using Reflex.Attributes;

public class CoinTimerRechargeableButton : RechargeableButton
{
    [Inject]
    private void Inject(CoinAdTimer adTimer) => Initialize(adTimer);
}
