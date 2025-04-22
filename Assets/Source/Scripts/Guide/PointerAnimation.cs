using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class PointerAnimation : MonoBehaviour
{
    private const int LoopCount = 3;

    [SerializeField] private float _animationDuration;
    [SerializeField] private float _scaleMultiplier;

    private void OnEnable()
    {
        LMotion.Create(transform.localScale, transform.localScale * _scaleMultiplier, _animationDuration)
            .WithLoops(LoopCount, LoopType.Yoyo)
            .BindToLocalScale(transform);
    }
}