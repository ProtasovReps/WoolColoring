using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _appearDuration;

    private UIAnimator _animator;
    private Stopwatch _stopwatch;
    private float _finalAlpha;

    private void Awake()
        => _finalAlpha = _image.color.a;

    private void OnEnable()
    {
        _stopwatch.Stop();
        _animator.FadeAlpha(_image, _finalAlpha, _appearDuration);
    }

    private void OnDisable() => _stopwatch.StartCount().Forget();

    [Inject]
    private void Inject(UIAnimator animator, Stopwatch stopwatch)
    {
        _animator = animator;
        _stopwatch = stopwatch;
    }
}