using System;
using UnityEngine;

public class ColoredStringHolder : StringHolder
{
    public event Action ColorChanged;

    public ColoredStringHolder(ColorString[] strings) : base(strings) { }

    public Color Color { get; private set; }

    public void SetColor(Color color)
    {
        Color = color;
        ColorChanged?.Invoke();
    }

    public IColorable[] GetAllStrings()
    {
        var colorStrings = new IColorable[StringCount];

        for (int i = colorStrings.Length - 1; i >= 0; i--)
            colorStrings[i] = GetString();

        return colorStrings;
    }

    protected override void PrepareString(IColorSettable freeString, IColorable newString)
    {
        Color newColor = newString.Color;

        if (newColor != Color)
            throw new ArgumentException(nameof(newString));

        freeString.SetColor(newColor);
    }

    protected override bool IsValidString(IColorable colorString) => true;
}
