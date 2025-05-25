using Reflex.Attributes;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = Input.PlayerInput;

namespace ClickReaders
{
    public abstract class ClickReader : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private float _maxRaycastDistance;

        private PlayerInput _playerInput;

        public bool IsPaused { get; private set; }

        [Inject]
        private void Inject(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        private void Awake()
        {
            _playerInput.PlayerClick.Click.performed += OnClickPerformed;
        }

        private void OnDestroy()
        {
            _playerInput.PlayerClick.Click.performed -= OnClickPerformed;
        }

        public virtual void SetPause(bool isPaused)
        {
            IsPaused = isPaused;
        }

        protected abstract void ValidateHit(RaycastHit hit);

        private void OnClickPerformed(InputAction.CallbackContext context)
        {
            if (IsPaused)
                return;

            Ray ray = _mainCamera.ScreenPointToRay(_playerInput.PlayerClick.ScreenPosition.ReadValue<Vector2>());

            if (Physics.Raycast(ray, out RaycastHit hit, _maxRaycastDistance, _layer) == false)
                return;

            ValidateHit(hit);
        }
    }
}