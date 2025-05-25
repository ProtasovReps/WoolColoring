using Ami.BroAudio;
using UnityEngine;
using UnityEngine.UI;

namespace LevelInterface.Buttons
{
    public abstract class ButtonView : Activatable
    {
        [SerializeField] private Button _button;
        [SerializeField] private SoundID _clickSound;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }

        public override void Activate()
        {
            _button.interactable = true;
        }

        public override void Deactivate()
        {
            _button.interactable = false;
        }

        protected virtual void OnButtonClick()
        {
            if (_clickSound != 0)
                BroAudio.Play(_clickSound);
        }
    }
}