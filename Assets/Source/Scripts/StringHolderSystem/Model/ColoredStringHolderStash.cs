using System;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using CustomInterface;

namespace StringHolders.Model
{
    public class ColoredStringHolderStash
    {
        private readonly Dictionary<bool, List<ColoredStringHolder>> _stringHolders;

        public ColoredStringHolderStash(ColoredStringHolder[] stringHolders, int activeCount)
        {
            if (stringHolders == null)
                throw new NullReferenceException(nameof(stringHolders));

            if (stringHolders.Length < activeCount)
                throw new ArgumentOutOfRangeException(nameof(activeCount));

            if (activeCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(activeCount));

            _stringHolders = new Dictionary<bool, List<ColoredStringHolder>>();

            FillDictionary(stringHolders, activeCount);
        }

        public int ActiveCount => _stringHolders[true].Count;
        public IEnumerable<IFillable<ColoredStringHolder>> ActiveHolders => _stringHolders[true];
        public IEnumerable<IFillable<ColoredStringHolder>> InactiveHolders => _stringHolders[false];

        public ColoredStringHolder UnlockHolder()
        {
            if (_stringHolders[false].Count == 0)
                throw new InvalidOperationException(nameof(_stringHolders));

            ColoredStringHolder unlockedHolder = _stringHolders[false][0];

            _stringHolders[false].Remove(unlockedHolder);
            _stringHolders[true].Add(unlockedHolder);
            return unlockedHolder;
        }

        public void DeactivateHolder(ColoredStringHolder stringHolder)
        {
            if (_stringHolders[true].Contains(stringHolder) == false)
                throw new InvalidOperationException(nameof(stringHolder));

            _stringHolders[true].Remove(stringHolder);
            _stringHolders[false].Add(stringHolder);

            stringHolder.SetColor(ColorStates.InactiveColor);
        }

        public bool TryGetColoredStringHolder(Color requiredColor, out ColoredStringHolder holder)
        {
            holder = GetActiveHolder(requiredColor);
            return holder != null;
        }

        private void FillDictionary(ColoredStringHolder[] stringHolders, int activeCount)
        {
            ColoredStringHolder stringHolder;

            _stringHolders.Add(true, new List<ColoredStringHolder>());
            _stringHolders.Add(false, new List<ColoredStringHolder>());

            for (int i = 0; i < stringHolders.Length; i++)
            {
                stringHolder = stringHolders[i];

                if (i < activeCount)
                {
                    _stringHolders[true].Add(stringHolder);
                }
                else
                {
                    _stringHolders[false].Add(stringHolder);
                    stringHolder.SetColor(ColorStates.InactiveColor);
                }
            }
        }

        private ColoredStringHolder GetActiveHolder(Color requiredColor)
        {
            ColoredStringHolder stringHolder;
            int activeCount = _stringHolders[true].Count;

            for (int i = 0; i < activeCount; i++)
            {
                stringHolder = _stringHolders[true][i];

                if (stringHolder.Color == requiredColor)
                {
                    if (stringHolder.IsEnabled)
                    {
                        return stringHolder;
                    }
                }
            }

            return null;
        }
    }
}