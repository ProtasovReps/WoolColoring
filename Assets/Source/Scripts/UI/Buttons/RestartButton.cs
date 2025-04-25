using Reflex.Attributes;
using UnityEngine.SceneManagement;

public class RestartButton : SceneInteractionButton
{
    private ProgressSaver _progressSaver;

    [Inject]
    private void Inject(ProgressSaver progressSaver)
    {
        _progressSaver = progressSaver;
    }

    protected override void LoadScene()
    {
        _progressSaver.Save();
        Scene targetScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(targetScene.name, LoadSceneMode.Single);
    }
}