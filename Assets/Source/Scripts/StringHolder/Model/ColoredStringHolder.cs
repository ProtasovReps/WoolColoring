using System;
using UnityEngine;

public class ColoredStringHolder : StringHolder
{
    public event Action ColorChanged;

    public ColoredStringHolder(ColorString[] strings) : base(strings) { }

    public Color RequiredColor { get; private set; }

    public void SetRequiredColor(Color requiredColor)
    {
        RequiredColor = requiredColor;
        ColorChanged?.Invoke();
    }

    protected override void PrepareString(IColorable freeString, IColorable newString)
    {
        Color newColor = newString.Color;

        if (newColor != RequiredColor)
            throw new ArgumentException(nameof(newString));

        freeString.SetColor(newColor);
    }
}
