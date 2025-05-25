using CustomInterface;
using UnityEngine.SceneManagement;
using YG;

namespace YandexGamesSDK.Saves
{
    public class UnlockedLevelsSaver : ISaver
    {
        public void Save()
        {
            if (YG2.saves.UnlockedLevelsCount >= SceneManager.sceneCountInBuildSettings - 1)
                return;

            YG2.saves.UnlockedLevelsCount++;
        }
    }
}