using UnityEngine.SceneManagement;

namespace LevelInterface.Buttons
{
    public class SwitchSceneButton : SceneInteractionButton
    {
        private const int FirstSceneId = 3;

        public void Switch()
        {
            OnButtonClick();
        }

        protected override void LoadScene()
        {
            int sceneId = SceneManager.GetActiveScene().buildIndex;

            sceneId++;

            if (sceneId >= SceneManager.sceneCountInBuildSettings)
                sceneId = FirstSceneId;

            SceneManager.LoadScene(sceneId, LoadSceneMode.Single);
        }
    }
}