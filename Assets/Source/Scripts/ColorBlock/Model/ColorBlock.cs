using UnityEngine;
using System;

public class ColorBlock : IColorSettable
{
    private readonly Color _requiredColor;

    public event Action<Color> ColorSetted;

    public ColorBlock(Color requiredColor)
    {
        _requiredColor = requiredColor;
    }

    public Color RequiredColor => _requiredColor;

    public void SetColor(Color color)
    {
        if (color != _requiredColor)
            throw new ArgumentException(nameof(color));

        ColorSetted?.Invoke(color);
    }
}
