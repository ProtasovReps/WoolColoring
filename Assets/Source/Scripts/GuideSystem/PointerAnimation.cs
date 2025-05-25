using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace PlayerGuide
{
    public class PointerAnimation : MonoBehaviour
    {
        private const int LoopCount = 4;

        [SerializeField] private float _appearDuration = 0.8f;
        [SerializeField] private float _animationDuration;
        [SerializeField] private float _scaleMultiplier;

        private void OnEnable()
        {
            LMotion.Create(transform.localScale, transform.localScale * _scaleMultiplier, _animationDuration)
                .WithLoops(LoopCount, LoopType.Yoyo)
                .BindToLocalScale(transform);
        }

        public void PopUp(Transform transform)
        {
            LMotion.Create(Vector3.zero, transform.localScale, _appearDuration)
            .WithEase(Ease.OutElastic)
                .BindToLocalScale(transform);
        }
    }
}