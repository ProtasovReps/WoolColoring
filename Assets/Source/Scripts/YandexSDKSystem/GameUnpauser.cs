using Ami.BroAudio;
using Input;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

namespace YandexGamesSDK
{
    public class GameUnpauser : MonoBehaviour
    {
        [SerializeField] private EventSystem _eventSystem;
        [SerializeField] private SoundID _soundID;

        private PlayerInput _playerInput;

        [Inject]
        private void Inject(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        private void Awake()
        {
            YG2.onCloseAnyAdv += Unpause;
        }

        private void OnDestroy()
        {
            YG2.onCloseAnyAdv -= Unpause;
        }

        private void Unpause()
        {
            Time.timeScale = 1;
            _eventSystem.enabled = true;
            _playerInput.PlayerClick.Enable();
            BroAudio.Stop(BroAudioType.Music);
            BroAudio.Play(_soundID);
        }
    }
}