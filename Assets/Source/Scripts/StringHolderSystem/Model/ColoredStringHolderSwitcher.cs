using System;
using System.Collections.Generic;
using UnityEngine;
using BlockPicture.Model;

namespace StringHolders.Model
{
    public class ColoredStringHolderSwitcher
    {
        private readonly Picture _picture;
        private readonly ColoredStringHolderStash _stash;
        private readonly List<Color> _usedColors;

        public ColoredStringHolderSwitcher(Picture picture, ColoredStringHolderStash stash)
        {
            if (picture == null)
                throw new ArgumentNullException(nameof(picture));

            if (stash == null)
                throw new ArgumentNullException(nameof(stash));

            _picture = picture;
            _usedColors = new List<Color>();
            _stash = stash;
        }

        public event Action<ColoredStringHolder> HolderSwitched;

        public void Switch(ColoredStringHolder coloredHolder)
        {
            if (_usedColors.Contains(coloredHolder.Color))
            {
                _usedColors.Remove(coloredHolder.Color);
            }

            if (_picture.RequiredColorsCount < _stash.ActiveCount)
            {
                _stash.DeactivateHolder(coloredHolder);
                return;
            }

            Color requiredColor;

            do
            {
                if (_picture.GetRandomColor(out Color color) == false)
                {
                    return;
                }

                requiredColor = color;
            }
            while (_usedColors.Contains(requiredColor));

            _usedColors.Add(requiredColor);
            coloredHolder.SetColor(requiredColor);
            HolderSwitched?.Invoke(coloredHolder);
        }
    }
}