using Ami.BroAudio;
using UnityEngine;

public class SoundButton : ButtonView
{
    [SerializeField] private SoundID _sound;

    protected override void OnButtonClick() => BroAudio.Play(_sound);
}
