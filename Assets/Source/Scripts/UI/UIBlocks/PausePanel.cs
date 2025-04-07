using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _appearDuration;

    private UIAnimator _animator;
    private float _finalAlpha;

    private void Awake()
        => _finalAlpha = _image.color.a;

    private void OnEnable()
       => _animator.FadeAlpha(_image, _finalAlpha, _appearDuration);

    [Inject]
    private void Inject(UIAnimator animator)
        => _animator = animator;
}