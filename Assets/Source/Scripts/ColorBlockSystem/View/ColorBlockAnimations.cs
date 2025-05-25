using LitMotion;
using LitMotion.Extensions;
using System;
using UnityEngine;

namespace ColorBlocks.View
{
    public class ColorBlockAnimations : MonoBehaviour
    {
        [SerializeField] private float _increaseDelay = 0.2f;
        [SerializeField] private float _animationDuration = 0.2f;
        [SerializeField] private float _targetScaleMultiplier = 0.7f;

        public void Decrease(Transform transform)
        {
            Vector3 startScale = transform.localScale;
            Vector3 targetScale = transform.localScale * _targetScaleMultiplier;
            Action onComplete = () => Increase(transform, startScale);

            LMotion.Create(transform.localScale, targetScale, _animationDuration)
                .WithEase(Ease.InQuart)
                .WithOnComplete(onComplete)
                .BindToLocalScale(transform);
        }

        private void Increase(Transform transform, Vector3 startScale)
        {
            LMotion.Create(transform.localScale, startScale, _animationDuration)
            .WithEase(Ease.InQuart)
                .WithDelay(_increaseDelay)
                .BindToLocalScale(transform);
        }
    }
}