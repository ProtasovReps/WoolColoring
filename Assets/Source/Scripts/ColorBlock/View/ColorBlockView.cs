using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorBlockView : MonoBehaviour, IColorSettable
{
    [SerializeField] private Color _requiredColor;
    [SerializeField] private ColorView _colorView;

    private Transform _transform;

    public event Action<ColorBlockView> Colloring;

    public Color RequiredColor => _requiredColor;
    public Transform Transform => _transform;

    private void Awake()
    {
        _colorView.Initialize();
        _transform = transform;
    }

    public void SetColor(Color color)
    {
        Colloring?.Invoke(this);

        _colorView.SetColor(color);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _requiredColor;
        Gizmos.DrawCube(transform.position, transform.localScale / 2f);
    }
}
