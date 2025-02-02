using UnityEngine;
using System;

[RequireComponent(typeof(MeshRenderer))]
public class ColorBlock : MonoBehaviour
{
    [SerializeField] private Color _requiredColor;

    private MeshRenderer _renderer;

    public Color RequiredColor => _requiredColor;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void SetColor(Color color)
    {
        if (color != _requiredColor)
            throw new ArgumentException(nameof(color));

        _renderer.material.color = color;
    }
}
