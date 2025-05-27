using Ami.BroAudio;
using UnityEngine;

namespace ColorBlockSystem.View
{
    public class BlockSoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundID _coloringSound;

        public void Play()
        {
            BroAudio.Play(_coloringSound);
        }
    }
}