using UnityEngine.SceneManagement;

namespace LevelInterface.Buttons
{
    public class RestartButton : SceneInteractionButton
    {
        protected override void LoadScene()
        {
            Scene targetScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(targetScene.name, LoadSceneMode.Single);
        }
    }
}