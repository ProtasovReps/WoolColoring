using LitMotion;
using UnityEngine;
using UnityEngine.UI;

public class ScrollAnimation : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;
    [SerializeField] private float _animationDuration;
    [SerializeField] private float _animationDelay;

    private void OnEnable() => ScrollThrough();

    private void ScrollThrough()
    {
        LMotion.Create(0f, 1f, _animationDuration)
            .WithEase(Ease.OutElastic)
            .WithDelay(_animationDelay)
            .Bind(x => _scrollbar.value = x);
    }
}