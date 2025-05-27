using Cysharp.Threading.Tasks;
using UISystem;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using YG;
using Random = UnityEngine.Random;

namespace Extensions.View
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class LevelTransitionAnimation : MonoBehaviour
    {
        [SerializeField] private float _startScale = 10f;
        [SerializeField] private float _endScale = 0f;
        [SerializeField] private float _transitionSpeed;
        [SerializeField] private Color[] _colors;
        [SerializeField] private TemporaryActivatableUI _levelNumber;

        private Transform _transform;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _transform = transform;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void FadeIn()
        {
            LMotion.Create(Vector3.one * _startScale, Vector3.one * _endScale, _transitionSpeed)
                .WithEase(Ease.InOutCirc)
                .WithOnComplete(CompleteTransition)
                .BindToLocalScale(_transform);
        }

        public async UniTask FadeOut()
        {
            var randomIndex = Random.Range(0, _colors.Length);
            _spriteRenderer.color = _colors[randomIndex];

            await LMotion.Create(Vector3.one * _endScale, Vector3.one * _startScale, _transitionSpeed)
                .WithEase(Ease.InOutCirc)
                .BindToLocalScale(_transform);
        }

        private void CompleteTransition()
        {
            YG2.GameReadyAPI();
            _levelNumber.Activate();
        }
    }
}