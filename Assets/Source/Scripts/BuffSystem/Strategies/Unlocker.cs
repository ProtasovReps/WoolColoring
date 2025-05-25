using System;
using BlockPicture.Model;
using CustomInterface;
using Extensions;
using StringHolders.Model;

namespace Buffs.Strategies
{
    public class Unlocker : IBuff
    {
        private const int MaxStringHolderCount = 4;

        private readonly ColoredStringHolderStash _coloredStash;
        private readonly ColoredStringHolderSwitcher _coloredStringHolderSwitcher;
        private readonly Picture _picture;

        public Unlocker(ColoredStringHolderStash stash, ColoredStringHolderSwitcher switcher, Picture picture)
        {
            if (stash == null)
                throw new ArgumentNullException(nameof(stash));

            if (switcher == null)
                throw new ArgumentNullException(nameof(switcher));

            if (picture == null)
                throw new ArgumentNullException(nameof(picture));

            _coloredStringHolderSwitcher = switcher;
            _coloredStash = stash;
            _picture = picture;
        }

        public int Price => BuffPrices.UnlockHolderPrice;
        public string Id => RewardIds.Unlocker;

        public bool Validate()
        {
            return _picture.RequiredColorsCount > _coloredStash.ActiveCount
                   && _coloredStash.ActiveCount < MaxStringHolderCount;
        }

        public void Execute()
        {
            if (Validate() == false)
                throw new InvalidOperationException(nameof(Validate));

            ColoredStringHolder unlockedHolder = _coloredStash.UnlockHolder();

            _coloredStringHolderSwitcher.Switch(unlockedHolder);
        }
    }
}