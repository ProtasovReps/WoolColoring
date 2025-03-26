using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _appearDuration;

    private UIAnimator _animator;
    private BoltClickReader _boltClickReader;
    private FigureClickReader _figureClickReader;
    private float _finalAlpha;
    private bool _lastFigureClickReaderState;

    private void Awake()
    {
        _finalAlpha = _image.color.a;
    }

    private void OnEnable()
    {
        _lastFigureClickReaderState = _figureClickReader.IsPaused;
        Time.timeScale = 0f;

        _figureClickReader.SetPause(true);
        _boltClickReader.SetPause(true);

        _animator.FadeAlpha(_image, _finalAlpha, _appearDuration);
    }

    private void OnDisable()
    {
        _figureClickReader.SetPause(_lastFigureClickReaderState);
        Time.timeScale = 1f;
    }

    [Inject]
    private void Inject(BoltClickReader boltReader, FigureClickReader figureClickReader, UIAnimator animator)
    {
        _boltClickReader = boltReader;
        _figureClickReader = figureClickReader;
        _animator = animator;
    }
}