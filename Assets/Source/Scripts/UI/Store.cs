using Ami.BroAudio;
using UnityEngine;

public class Store : ActivatableUI
{
    [SerializeField] private SoundID _storeMusic;
    [SerializeField] private SoundID _mainMusic;

    public override void Activate()
    {
        BroAudio.Pause(_mainMusic);
        BroAudio.Play(_storeMusic);
        base.Activate();
    }

    public override void Deactivate()
    {
        BroAudio.Stop(_storeMusic);
        BroAudio.UnPause(_mainMusic);
        base.Deactivate();
    }
}
