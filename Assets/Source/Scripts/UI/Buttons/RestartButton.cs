using UnityEngine.SceneManagement;

public class RestartButton : SceneInteractionButton
{
    protected override void LoadScene()
    {
        Scene targetScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(targetScene.name, LoadSceneMode.Single);
    }
}