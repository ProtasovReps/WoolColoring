using System;
using System.Collections.Generic;
using ColorStrings.Model;
using CustomInterface;
using Extensions;
using StringHolders.Model;
using Random = UnityEngine.Random;

namespace Buffs.Strategies
{
    public class Filler : IBuff
    {
        private const int MinActiveCount = 2;

        private readonly ColoredStringHolder[] _coloredStringHolders;
        private readonly ColorString _colorString;

        public Filler(ColoredStringHolder[] holders)
        {
            if (holders == null)
                throw new ArgumentNullException(nameof(holders));

            if (holders.Length == 0)
                throw new EmptyCollectionException();

            _coloredStringHolders = holders;
            _colorString = new ColorString();
        }

        public int Price => BuffPrices.FillHolderPrice;
        public string Id => RewardIds.Filler;

        public void Execute()
        {
            List<ColoredStringHolder> validHolders = new();
            int randomIndex;

            for (int i = 0; i < _coloredStringHolders.Length; i++)
            {
                if (IsValidHolder(_coloredStringHolders[i]))
                    validHolders.Add(_coloredStringHolders[i]);
            }

            if (validHolders.Count == 0)
                throw new InvalidOperationException(nameof(validHolders));

            randomIndex = Random.Range(0, validHolders.Count);

            FillHolder(validHolders[randomIndex]);
        }

        public bool Validate()
        {
            int activeCount = 0;

            for (int i = 0; i < _coloredStringHolders.Length; i++)
            {
                if (IsValidHolder(_coloredStringHolders[i]))
                    activeCount++;
            }

            return activeCount >= MinActiveCount;
        }

        private bool IsValidHolder(ColoredStringHolder holder)
        {
            return holder.Color != ColorStates.InactiveColor && holder.IsEnabled == true;
        }

        private void FillHolder(ColoredStringHolder holder)
        {
            int needStringCount = holder.MaxStringCount - holder.StringCount;

            holder.SetEnabled(false);
            _colorString.SetColor(holder.Color);

            for (int i = 0; i < needStringCount; i++)
                holder.Add(_colorString);

            holder.SetEnabled(true);
        }
    }
}