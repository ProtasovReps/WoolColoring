using Ami.BroAudio;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private SoundID _soundID;

    public void Play() => BroAudio.Play(_soundID);

    public void Stop() => BroAudio.Stop(_soundID);
}