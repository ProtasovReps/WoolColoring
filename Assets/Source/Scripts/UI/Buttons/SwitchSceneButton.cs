using UnityEngine.SceneManagement;

public class SwitchSceneButton : SceneInteractionButton
{
    private const int FirstSceneId = 1;

    protected override void LoadScene()
    {
        int sceneId = SceneManager.GetActiveScene().buildIndex;

        if (sceneId == SceneManager.sceneCountInBuildSettings - 1)
            sceneId = FirstSceneId;
        else
            sceneId++;

        SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
    }
}