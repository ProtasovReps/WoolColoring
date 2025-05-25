using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Extensions
{
    public class ColorPallete
    {
        private readonly Color[] _colors;

        private Color _lastColor;

        public ColorPallete(Color[] colors)
        {
            if (colors == null)
                throw new ArgumentNullException(nameof(colors));

            _colors = colors;
        }

        public Color GetRandomColor()
        {
            Color color = _lastColor;

            while (color == _lastColor)
                color = _colors[Random.Range(0, _colors.Length)];

            _lastColor = color;
            return color;
        }
    }
}