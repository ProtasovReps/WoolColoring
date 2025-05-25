using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace LevelInterface.Buttons
{
    public class LevelSelectButton : SceneInteractionButton
    {
        [SerializeField, Min(1)] private int _levelNumber;
        [SerializeField] private Image _padlock;

        public int LevelNumber => _levelNumber;

        private void Awake()
        {
            if (YG2.saves.PassedLevelIndexes.Contains(LevelNumber) == false)
                Deactivate();

            if (YG2.saves.PassedLevelIndexes.Contains(LevelNumber - 1))
                Activate();
        }

        public override void Activate()
        {
            _padlock.enabled = false;
            base.Activate();
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
}