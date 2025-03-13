using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private BoltClickReader _boltClickReader;
    [SerializeField] private float _appearDuration;

    private float _finalAlpha;

    private void Awake()
    {
        _finalAlpha = _image.color.a;
    }

    private void OnEnable()
    {
        _boltClickReader.SetPause(true);

        LMotion.Create(0f, _finalAlpha, _appearDuration)
            .WithScheduler(MotionScheduler.UpdateIgnoreTimeScale)
            .BindToColorA(_image);
    }

    private void OnDisable()
    {
        _boltClickReader.SetPause(false);
    }
}
