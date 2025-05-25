using Ami.BroAudio;
using UnityEngine;

namespace StringHolders.View
{
    public class HolderSoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundID _switchSound;
        [SerializeField] private SoundID _filledSound;

        public void Switch()
        {
            BroAudio.Play(_switchSound);
        }

        public void Fill()
        {
            BroAudio.Play(_filledSound);
        }
    }
}
