using UISystem.Timers;
using Reflex.Attributes;

namespace UISystem.Buttons
{
    public class CoinTimerRechargeableButton : RechargeableButton
    {
        [Inject]
        private void Inject(CoinAdTimer adTimer)
        {
            Initialize(adTimer);
        }
    }
}
