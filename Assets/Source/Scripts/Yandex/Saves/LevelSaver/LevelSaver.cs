using UnityEngine.SceneManagement;
using YG;

public class LevelSaver : ISaver
{
    public void Save()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex >= SceneManager.sceneCountInBuildSettings - 1)
            sceneIndex = 0;
        else
            sceneIndex++;

        YG2.saves.LastLevelIndex = sceneIndex;
    }
}
