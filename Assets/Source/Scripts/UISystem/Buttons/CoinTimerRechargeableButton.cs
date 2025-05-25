using LevelInterface.Timers;
using Reflex.Attributes;

namespace LevelInterface.Buttons
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
