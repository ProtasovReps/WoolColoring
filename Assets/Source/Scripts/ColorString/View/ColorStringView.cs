using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

[RequireComponent(typeof(ColorView))]
[RequireComponent(typeof(TransformView))]
[RequireComponent(typeof(ActiveStateSwitcher))]
public class ColorStringView : MonoBehaviour, IColorSettable, IColorable
{
    [SerializeField] private float _appearDuration = 0.2f;
    [SerializeField] private float _disappearDuration = 1f;

    private float _animationDelay;
    private ColorView _colorView;
    private TransformView _transformView;
    private ActiveStateSwitcher _stateSwitcher;

    public Color Color { get; private set; }

    public void Initialize()
    {
        _animationDelay = _appearDuration + _disappearDuration;
        _stateSwitcher = GetComponent<ActiveStateSwitcher>();
        _transformView = GetComponent<TransformView>();
        _colorView = GetComponent<ColorView>();

        _transformView.Initialize();
        _colorView.Initialize();
        _stateSwitcher.Initialize();
    }

    public void SetColor(Color color)
    {
        Color = color;
        _colorView.SetColor(color);
    }

    public void Appear()
    {
        _stateSwitcher.SetActive(true);
        LMotion.Create(_transformView.Transform.localScale, _transformView.StartScale, _appearDuration).BindToLocalScale(_transformView.Transform);
    }

    public void Disappear()
    {
        LMotion.Create(_transformView.Transform.localScale, Vector3.zero, _disappearDuration).WithDelay(_animationDelay).WithOnComplete(() => _stateSwitcher.SetActive(false)).BindToLocalScale(_transformView.Transform);
    }
}