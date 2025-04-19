using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class UnlockedLevelsSaver : ISaver
{
    public void Save()
    {
        if (YG2.saves.UnlockedLevelsCount >= SceneManager.sceneCountInBuildSettings - 1)
        {
            Debug.Log("����");
            return;
        }

        Debug.Log("��������");
        YG2.saves.UnlockedLevelsCount++;
        Debug.Log(YG2.saves.UnlockedLevelsCount);
    }
}