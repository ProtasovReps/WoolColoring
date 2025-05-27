using UISystem.Timers;
using Reflex.Attributes;

namespace UISystem
{
    public class ButtonRechargeAppearText : TemporaryActivatableUI
    {
        private AdTimer _timer;

        [Inject]
        private void Inject(CoinAdTimer adTimer)
        {
            _timer = adTimer;
            _timer.TimeElapsed += Activate;
        }

        private void OnDestroy()
        {
            _timer.TimeElapsed -= Activate;
        }
    }
}