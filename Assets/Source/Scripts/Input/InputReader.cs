using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private PlayerInput _playerInput;

    public event Action<RaycastHit> Clicked;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.PlayerClick.Enable();
        _playerInput.PlayerClick.Click.performed += OnClickPerformed;
    }

    private void OnDisable()
    {
        _playerInput.PlayerClick.Disable();
        _playerInput.PlayerClick.Click.performed -= OnClickPerformed;
    }

    private void OnClickPerformed(InputAction.CallbackContext context)
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(_playerInput.PlayerClick.ScreenPosition.ReadValue<Vector2>()), out RaycastHit hit))
            Clicked?.Invoke(hit);
    }
}
