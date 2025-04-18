using UnityEngine.SceneManagement;
using YG;

public class LevelSaver : ISaver
{
    public void Save()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        sceneIndex++;

        if (sceneIndex >= SceneManager.sceneCountInBuildSettings)
            sceneIndex = 0;

        YG2.saves.LastLevelIndex = sceneIndex;
    }
}