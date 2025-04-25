using Reflex.Attributes;

public class ButtonRechargeAppearText : TemporaryActivatableUI
{
    private AdTimer _timer;

    [Inject]
    private void Inject(AdTimer adTimer)
    {
        _timer = adTimer;
        _timer.TimeElapsed += Activate;
    }

    private void OnDestroy()
    {
        _timer.TimeElapsed -= Activate;
    }
}