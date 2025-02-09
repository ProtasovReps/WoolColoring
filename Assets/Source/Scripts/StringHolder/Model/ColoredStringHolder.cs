using System;
using UnityEngine;

public class ColoredStringHolder : StringHolder, IColorSettable
{
    public event Action ColorChanged;

    public ColoredStringHolder(ColorString[] strings) : base(strings) { }

    public Color RequiredColor { get; private set; }

    public void SetColor(Color color)
    {
        RequiredColor = color;
        ColorChanged?.Invoke();
    }

    protected override void PrepareString(IColorSettable freeString, IColorable newString)
    {
        Color newColor = newString.Color;

        if (newColor != RequiredColor)
            throw new ArgumentException(nameof(newString));

        freeString.SetColor(newColor);
    }
}
