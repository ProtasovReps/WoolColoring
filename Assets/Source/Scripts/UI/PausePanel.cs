using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private BoltClickReader _boltClickReader;
    [SerializeField] private FigureClickReader _figureClickReader;
    [SerializeField] private float _appearDuration;

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

        LMotion.Create(0f, _finalAlpha, _appearDuration)
            .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
            .BindToColorA(_image);
    }

    private void OnDisable()
    {
        _figureClickReader.SetPause(_lastFigureClickReaderState);
        Time.timeScale = 1f;
    }
}