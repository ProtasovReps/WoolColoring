using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class LevelSelectButton : SceneInteractionButton
{
    [SerializeField, Min(1)] private int _levelNumber;
    [SerializeField] private Image _padlock;

    public int LevelNumber => _levelNumber;

    private void Awake()
    {
        if (YG2.saves.UnlockedLevelsCount < _levelNumber - 1)
            Deactivate();
    }

    public override void Deactivate()
    {
        _padlock.enabled = true;
        base.Deactivate();
    }

    protected override void LoadScene()
    {
        SceneManager.LoadScene(_levelNumber, LoadSceneMode.Single);
    }
}
