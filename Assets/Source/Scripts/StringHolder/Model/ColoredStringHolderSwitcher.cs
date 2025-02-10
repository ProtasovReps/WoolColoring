using System;
using System.Collections.Generic;
using UnityEngine;

public class ColoredStringHolderSwitcher
{
    private readonly Picture _picture;
    private readonly int _minPictureColorCount = 1;
    private List<Color> _usedColors;

    public event Action<ColoredStringHolder> ColorSwitched;

    public ColoredStringHolderSwitcher(Picture picture)
    {
        if (picture == null)
            throw new ArgumentNullException(nameof(picture));

        _picture = picture;
        _usedColors = new List<Color>();
    }

    public void ChangeStringHolderColor(ColoredStringHolder coloredHolder)
    {
        if (_picture.RequiredColorsCount <= _minPictureColorCount)
        {
            coloredHolder.SetColor(Color.white);
            ColorSwitched?.Invoke(coloredHolder);
            return;
        }

        Color requiredColor = _picture.GetRandomColor();

        while (_usedColors.Contains(requiredColor))
        {
            requiredColor = _picture.GetRandomColor();
        }

        _usedColors.Add(requiredColor);
        coloredHolder.SetColor(requiredColor);
        ColorSwitched?.Invoke(coloredHolder);
    }
}
