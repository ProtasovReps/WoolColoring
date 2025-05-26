using System;
using UnityEngine;
using CustomInterface;
using Reflex.Attributes;
using Extensions.View;

namespace ColorBlocks.View
{
    public class ColorBlockView : MonoBehaviour, IColorSettable
    {
        [SerializeField] private Color _requiredColor;
        [SerializeField] private ColorView _colorView;
        [SerializeField] private float _gizmosSize;

        private Transform _transform;
        private ColorBlockAnimations _animations;
        private BlockSoundPlayer _soundPlayer;

        public event Action<ColorBlockView> Coloring;

        public Color RequiredColor => _requiredColor;
        public Transform Transform => _transform;

        [Inject]
        private void Inject(ColorBlockAnimations animations, BlockSoundPlayer soundPlayer)
        {
            _animations = animations;
            _soundPlayer = soundPlayer;
        }

        private void Awake()
        {
            _colorView.Initialize();
            _transform = transform;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _requiredColor;
            Gizmos.DrawCube(transform.position, transform.localScale * _gizmosSize);
        }

        public void SetColor(Color color)
        {
            if (color != _requiredColor)
                throw new InvalidOperationException(nameof(color));

            Coloring?.Invoke(this);
            _colorView.SetColor(color);
            _animations.Decrease(_transform);
            _soundPlayer.Play();
        }
    }
}