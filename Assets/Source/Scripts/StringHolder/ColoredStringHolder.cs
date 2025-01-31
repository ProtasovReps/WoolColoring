using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColoredStringHolder : StringHolder
{
    private MeshRenderer _meshRenderer;

    public Color RequiredColor { get; private set; }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void SetRequiredColor(Color requiredColor)
    {
        RequiredColor = requiredColor;
        _meshRenderer.material.color = RequiredColor;
    }

    protected override void PrepareString(IColorable freeString, IColorable newString)
    {
        if (newString.Color != RequiredColor)
            throw new ArgumentException(nameof(newString));

        freeString.SetColor(newString.Color);
    }
}
