using System;
using System.Collections.Generic;
using UnityEngine;

public class ColoredStringHolderSwitcher
{
    private readonly Picture _picture;
    private readonly ColoredStringHolderStash _stash;
    private readonly int _minPictureColorCount = 1;
    private readonly List<Color> _usedColors;

    public event Action<ColoredStringHolder> ColorSwitched;

    public ColoredStringHolderSwitcher(Picture picture, ColoredStringHolderStash stash)
    {
        if (picture == null)
            throw new ArgumentNullException(nameof(picture));

        if(stash == null)
            throw new ArgumentNullException(nameof(stash));

        _picture = picture;
        _usedColors = new List<Color>();
        _stash = stash;
    }

    public void ChangeStringHolderColor(ColoredStringHolder coloredHolder)
    {
        if (_picture.RequiredColorsCount <= _minPictureColorCount)
        {
            _stash.DeactivateHolder(coloredHolder);
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
