using Ami.BroAudio;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using YG;

public abstract class SceneInteractionButton : ButtonView
{
    private LevelTransitionAnimation _transitionAnimation;

    [Inject]
    private void Inject(LevelTransitionAnimation transition)
    {
        _transitionAnimation = transition;
    }

    protected override void OnButtonClick() => ValidateClick().Forget();

    protected abstract void LoadScene();

    private async UniTaskVoid ValidateClick()
    {
        YG2.RewardedAdvShow(name);
        Deactivate();
        BroAudio.Stop(BroAudioType.Music);
        base.OnButtonClick();
        await _transitionAnimation.FadeOut();
        LoadScene();
    }
}
