using UnityEngine;

public class MuteVolumeButton : ButtonView
{
    [SerializeField] private AudioListener _audioListener;

    private bool _isMuted = true;

    protected override void OnButtonClick()
    {
        _isMuted = !_isMuted;
        _audioListener.enabled = _isMuted;
    }
}