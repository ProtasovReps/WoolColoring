using System;
using System.Linq;
using UnityEngine;

public class ColoredStringHolder : StringHolder
{
    private Color _requiredColor; // пока что так, потом сделаем отдельный скрипт под цвета

    public Color RequiredColor => _requiredColor;

    private void Awake()
    {
        _requiredColor = GetComponent<MeshRenderer>().sharedMaterial.color;
    }

    protected override void PrepareString(ColorString freeString, ColorString newString)
    {
        if (Strings.First().Color != _requiredColor)
            throw new ArgumentException(nameof(newString));
    }
}
