using System;
using UnityEngine;
using ColorStrings.Model;
using CustomInterface;

namespace StringHolders.Model
{
    public class ColoredStringHolder : StringHolder, IFillable<ColoredStringHolder>
    {
        public ColoredStringHolder(ColorString[] strings) : base(strings)
        {
            SetEnabled(true);
        }

        public event Action ColorChanged;
        public event Action<ColoredStringHolder> Filled;

        public bool IsEnabled { get; private set; }
        public Color Color { get; private set; }

        public void SetColor(Color color)
        {
            Color = color;
            ColorChanged?.Invoke();
        }

        public void SetEnabled(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        public void GetLastString()
        {
            GetString();
        }

        protected override void PrepareString(IColorSettable freeString, IColorable newString)
        {
            Color newColor = newString.Color;

            if (newColor != Color)
                throw new ArgumentException(nameof(newString));

            freeString.SetColor(newColor);
        }

        protected override bool IsValidString(IColorable colorString)
        {
            return true;
        }

        protected override void OnFilled()
        {
            Filled?.Invoke(this);
        }
    }
}