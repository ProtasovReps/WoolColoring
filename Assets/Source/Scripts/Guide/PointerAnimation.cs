using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class PointerAnimation : MonoBehaviour
{
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _scaleMultiplier;

    private void OnEnable()
    {
        LMotion.Create(transform.localScale, transform.localScale * _scaleMultiplier, _animationDuration)
            .WithLoops(-1, LoopType.Yoyo)
            .BindToLocalScale(transform);
    }
}