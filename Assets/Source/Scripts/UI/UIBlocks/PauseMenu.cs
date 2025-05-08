using YG;

public class PauseMenu : ActivatableUI
{
    public override void Deactivate()
    {
        YG2.InterstitialAdvShow();
        base.Deactivate();
    }
}