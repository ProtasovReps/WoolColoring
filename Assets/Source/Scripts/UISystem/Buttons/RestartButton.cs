using UnityEngine.SceneManagement;

namespace UISystem.Buttons
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