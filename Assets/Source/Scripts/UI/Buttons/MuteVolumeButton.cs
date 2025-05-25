using UnityEngine;

namespace LevelInterface.Buttons
{
    public class MuteVolumeButton : ButtonView
    {
        private bool _isMuted;

        protected override void OnButtonClick()
        {
            _isMuted = !_isMuted;
            AudioListener.pause = _isMuted;
        }
    }
}