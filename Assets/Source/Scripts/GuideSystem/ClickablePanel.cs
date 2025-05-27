using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Reflex.Attributes;
using PlayerInput = Input.PlayerInput;

namespace GuideSystem
{
    public class ClickablePanel : MonoBehaviour
    {
        private PlayerInput _playerInput;

        public event Action Clicked;

        [Inject]
        private void Inject(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        private void OnEnable()
        {
            _playerInput.PlayerClick.Click.performed += OnPlayerClick;
        }

        private void OnDisable()
        {
            _playerInput.PlayerClick.Click.performed -= OnPlayerClick;
        }

        private void OnPlayerClick(InputAction.CallbackContext context)
        {
            Clicked?.Invoke();
        }
    }
}