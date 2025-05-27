using Interface;
using UnityEngine.SceneManagement;
using YG;

namespace Yandex.Saves
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