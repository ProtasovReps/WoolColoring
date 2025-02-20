using System;
using UnityEngine;

public class ColorString : IColorable, IColorSettable
{
    public event Action ColorSetted;
    public event Action EnableStateSwitched;

    public Color Color { get; private set; }
    public bool IsEnabled { get; private set; }

    public void SetEnable(bool isEnabled)
    {
        IsEnabled = isEnabled;
        EnableStateSwitched?.Invoke();
    }

    public void SetColor(Color color)
    {
        Color = color;
        ColorSetted?.Invoke();
    }
}