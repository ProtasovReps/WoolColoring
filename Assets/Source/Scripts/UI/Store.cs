using Ami.BroAudio;
using UnityEngine;

public class Store : ActivatableUI
{
    [SerializeField] private SoundID _storeMusic;
    [SerializeField] private SoundID _mainMusic;

    public override void Activate()
    {
        //BroAudio.Pause(_mainMusic, 0.1f);
        //BroAudio.Play(_storeMusic);

        base.Activate();
    }
    // почему-то не работает
    public override void Deactivate()
    {
        //BroAudio.Stop(_storeMusic);
        //BroAudio.UnPause(_mainMusic, 0.1f);

        base.Deactivate();
    }
}