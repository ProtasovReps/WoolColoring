using UnityEngine.SceneManagement;

public class SwitchSceneButton : SceneInteractionButton
{
    private const int FirstSceneId = 1;

    protected override void LoadScene()
    {
        int sceneId = SceneManager.GetActiveScene().buildIndex;

        sceneId++;

        if (sceneId >= SceneManager.sceneCountInBuildSettings)
            sceneId = FirstSceneId;

        SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
    }
}