using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneButton : SceneInteractionButton
{
    [SerializeField, Min(0)] private int _nextSceneId;

    protected override void LoadScene()
    {
       SceneManager.LoadScene(_nextSceneId, LoadSceneMode.Single);
    }
}
