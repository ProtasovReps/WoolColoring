using CustomInterface;
using UnityEngine.SceneManagement;
using YG;

namespace YandexGamesSDK.Saves.Level
{
    public class LevelSaver : ISaver
    {
        public void Save()
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;

            if (YG2.saves.PassedLevelIndexes.Contains(sceneIndex) == false)
            {
                YG2.saves.PassedLevelIndexes.Add(sceneIndex);
            }

            sceneIndex++;

            if (sceneIndex >= SceneManager.sceneCountInBuildSettings)
                sceneIndex = 0;

            YG2.saves.LastLevelIndex = sceneIndex;
        }
    }
}