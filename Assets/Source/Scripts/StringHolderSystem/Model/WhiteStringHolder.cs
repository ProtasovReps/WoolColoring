using System;
using UnityEngine;
using ColorStringSystem.Model;
using Interface;

namespace StringHolderSystem.Model
{
    public class WhiteStringHolder : StringHolder, IFillable<WhiteStringHolder>
    {
        private Color _requiredColor;

        public WhiteStringHolder(ColorString[] strings)
            : base(strings)
        {
        }

        public event Action<WhiteStringHolder> Filled;

        public IColorable GetColorable(Color color)
        {
            _requiredColor = color;
            return GetString();
        }

        public int GetColorCount(Color color)
        {
            int requiredColorsCount = 0;

            foreach (ColorString colorString in Strings)
            {
                if (IsEnabledString(colorString, false))
                    continue;

                if (colorString.Color != color)
                    continue;

                requiredColorsCount++;
            }

            return requiredColorsCount;
        }

        public void RemoveAllStrings()
        {
            foreach (ColorString colorString in Strings)
            {
                _requiredColor = colorString.Color;

                try
                {
                    GetString();
                }
                catch (InvalidOperationException)
                {
                    continue;
                }
            }
        }

        protected override void PrepareString(IColorSettable freeString, IColorable newString)
        {
            freeString.SetColor(newString.Color);
        }

        protected override bool IsValidString(IColorable colorString)
        {
            return colorString.Color == _requiredColor;
        }

        protected override void OnFilled()
        {
            Filled?.Invoke(this);
        }
    }
}