using YG;

namespace LevelInterface.Blocks
{
    public class PauseMenu : ActivatableUI
    {
        public override void Deactivate()
        {
            YG2.InterstitialAdvShow();
            base.Deactivate();
        }
    }
}