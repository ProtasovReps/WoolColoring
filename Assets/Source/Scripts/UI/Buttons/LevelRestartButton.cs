using Ami.BroAudio;
using Reflex.Attributes;
using UnityEngine.SceneManagement;

public class LevelRestartButton : ButtonView
{
    private LevelTransitionAnimation _transitionAnimation;

    [Inject]
    private void Inject(LevelTransitionAnimation transition)
    {
        _transitionAnimation = transition;
    }

    protected override void OnButtonClick()
    {
        BroAudio.Stop(BroAudioType.Music);
        _transitionAnimation.FadeOut(RestartScene);
        base.OnButtonClick();
    }

    private void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name, LoadSceneMode.Single);
    }
}
