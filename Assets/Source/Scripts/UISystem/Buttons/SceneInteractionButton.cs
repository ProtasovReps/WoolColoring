using Ami.BroAudio;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using Extensions.View;
using YG;

namespace UISystem.Buttons
{
    public abstract class SceneInteractionButton : ButtonView
    {
        private LevelTransitionAnimation _transitionAnimation;

        [Inject]
        private void Inject(LevelTransitionAnimation transition)
        {
            _transitionAnimation = transition;
        }

        protected abstract void LoadScene();

        protected override void OnButtonClick()
        {
            ValidateClick().Forget();
        }

        private async UniTaskVoid ValidateClick()
        {
            YG2.InterstitialAdvShow();
            Deactivate();
            BroAudio.Stop(BroAudioType.Music);
            base.OnButtonClick();
            await _transitionAnimation.FadeOut();
            LoadScene();
        }
    }
}
