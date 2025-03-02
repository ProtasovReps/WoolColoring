using System;
using UnityEngine;

public class ColoredStringHolder : StringHolder, IFillable<ColoredStringHolder>
{
    public event Action ColorChanged;
    public event Action<ColoredStringHolder> Filled;

    public ColoredStringHolder(ColorString[] strings) : base(strings) => SetEnabled(true);

    public bool IsEnabled { get; private set; }

    public Color Color { get; private set; }

    public void SetColor(Color color)
    {
        Color = color;
        ColorChanged?.Invoke();
    }

    public void SetEnabled(bool isEnabled) => IsEnabled = isEnabled;

    public IColorable GetLastString() => GetString();

    protected override void PrepareString(IColorSettable freeString, IColorable newString)
    {
        Color newColor = newString.Color;

        if (newColor != Color)
            throw new ArgumentException(nameof(newString));

        freeString.SetColor(newColor);
    }

    protected override bool IsValidString(IColorable colorString) => true;

    protected override void OnFilled() => Filled?.Invoke(this);
}
