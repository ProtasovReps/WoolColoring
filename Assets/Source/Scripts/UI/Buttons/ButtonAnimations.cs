using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

public class ButtonAnimations : MonoBehaviour
{
    [SerializeField] private float _targetScale = 0.1f;
    [SerializeField] private float _animationDuration = 0.1f;

    public void Interact(Transform transform)
    {
        LSequence.Create()
            .Append(LMotion.Create(transform.localScale, transform.localScale * _targetScale, _animationDuration)
            .WithEase(Ease.OutCirc)
            .BindToLocalScale(transform))
            .Append(LMotion.Create(transform.localScale * _targetScale, transform.localScale, _animationDuration)
            .WithEase(Ease.InCirc)
            .BindToLocalScale(transform))
            .Run();
    }
}
