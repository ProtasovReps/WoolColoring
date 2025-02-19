using LitMotion;
using LitMotion.Extensions;
using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorBlockView : MonoBehaviour, IColorSettable
{
    [SerializeField] private Color _requiredColor;
    [SerializeField] private ColorView _colorView;

    private Transform _transform;
    private Vector3 _startScale;

    public event Action<ColorBlockView> Colloring;

    public Color RequiredColor => _requiredColor;
    public Transform Transform => _transform;

    private void Awake()
    {
        _colorView.Initialize();
        _transform = transform;
        _startScale = _transform.localScale;
    }

    public void SetColor(Color color)
    {
        Colloring?.Invoke(this);

        Decrease();
        _colorView.SetColor(color);
    }

    private void Decrease()
    {
        LMotion.Create(_transform.localScale, _transform.localScale * 0.7f, 0.2f).WithEase(Ease.InQuart).WithOnComplete(Increase).BindToLocalScale(_transform);
    }

    private void Increase()
    {
        LMotion.Create(_transform.localScale, _startScale, 0.2f).WithEase(Ease.InQuart).WithDelay(0.5f).BindToLocalScale(_transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _requiredColor;
        Gizmos.DrawCube(transform.position, transform.localScale / 2f);
    }
}
