using Reflex.Attributes;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.EventSystems;
using YG;

public class GameUnpauser : MonoBehaviour
{
    [SerializeField] private EventSystem _eventSystem;

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
    }
}