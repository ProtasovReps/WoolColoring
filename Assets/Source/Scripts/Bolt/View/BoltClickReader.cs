using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BoltClickReader : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _maxRaycastDistance;

    private PlayerInput _playerInput;
    private BoltPressPresenter _presenter;

    public void Initialize(BoltPressPresenter presenter)
    {
        if (presenter == null)
            throw new ArgumentNullException(nameof(presenter));

        _presenter = presenter;
    }

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
        Ray ray = _mainCamera.ScreenPointToRay(_playerInput.PlayerClick.ScreenPosition.ReadValue<Vector2>());

        if (Physics.Raycast(ray, out RaycastHit hit, _maxRaycastDistance, _layer) == false)
            return;

        _presenter.ProcessClick(hit);
    }
}
