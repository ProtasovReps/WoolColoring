using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UISystem
{
    public class LevelNumberTextField : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private void Awake()
        {
            SetLevelNumber();
        }

        private void SetLevelNumber()
        {
            Scene currentScene = SceneManager.GetActiveScene();
            int currentLevelNumber = currentScene.buildIndex;

            _text.text = currentLevelNumber.ToString();
        }
    }
}