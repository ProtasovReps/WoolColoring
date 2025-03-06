using Ami.BroAudio;
using UnityEngine;

public class BlockSoundPlayer : MonoBehaviour
{
    [SerializeField] private SoundID _coloringSound;

    public void Play() => BroAudio.Play(_coloringSound);
}
