using Lean.Localization;
using Reflex.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class ProjectInstaller : MonoBehaviour, IInstaller
{
    private const int FirstLevelIndex = 1;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        var awaiter = GameObject.CreatePrimitive(PrimitiveType.Cube).AddComponent<SDKAwaiter>();

        awaiter.WaitSDKInitialization(InstallProject).Forget();
    }

    private void InstallProject()
    {
        InstallLocalization();
        InstallLevel();
    }

    private void InstallLocalization()
    {
        (string, string) russian = ("ru", "Russian");
        (string, string) turkish = ("tr", "Turkish");
        string english = "English";
        string newLanguage;
        string playerLanguage;

        YG2.GetLanguage();
        playerLanguage = YG2.lang;

        if (playerLanguage == russian.Item1)
            newLanguage = russian.Item2;
        else if (playerLanguage == turkish.Item1)
            newLanguage = turkish.Item2;
        else
            newLanguage = english;

        LeanLocalization.SetCurrentLanguageAll(newLanguage);
    }

    private void InstallLevel()
    {
        int levelIndex;
        int lastLevelIndex = YG2.saves.LastLevelIndex;

        if(lastLevelIndex == 0)
            levelIndex = FirstLevelIndex;
        else
            levelIndex = lastLevelIndex;

        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }
}