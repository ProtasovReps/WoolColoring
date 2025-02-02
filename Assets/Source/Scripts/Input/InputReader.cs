using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    private PlayerInput _playerInput;

    public event Action<RaycastHit> Clicked;

    public void Initialize()
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
        Ray ray = Camera.main.ScreenPointToRay(_playerInput.PlayerClick.ScreenPosition.ReadValue<Vector2>());

        if (Physics.Raycast(ray, out RaycastHit hit))
            Clicked?.Invoke(hit);
    }
}
