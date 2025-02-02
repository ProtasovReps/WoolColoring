using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColoredStringHolder : StringHolder
{
    private MeshRenderer _meshRenderer;

    public Color RequiredColor { get; private set; }

    public override void Initialize()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        base.Initialize();
    }

    public void SetRequiredColor(Color requiredColor)
    {
        RequiredColor = requiredColor;
        _meshRenderer.material.color = RequiredColor;
    }

    protected override void PrepareString(IColorable freeString, IColorable newString)
    {
        Color newColor = newString.GetColor();

        if (newColor != RequiredColor)
            throw new ArgumentException(nameof(newString));

        freeString.SetColor(newColor);
    }
}
