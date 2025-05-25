using Ami.BroAudio;
using UnityEngine;

namespace ColorBlocks.View
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