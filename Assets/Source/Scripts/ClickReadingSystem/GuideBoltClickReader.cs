using System;
using UnityEngine;
using Reflex.Attributes;
using Bolts.View;
using ColorStrings.Model;
using StringHolders.Model;

namespace ClickReaders
{
    public class GuideBoltClickReader : ClickReader
    {
        private StringDistributor _distributor;
        private ColoredStringHolderStash _coloredStringHolderStash;

        public event Action Unscrewed;

        public bool IsWhiteStringHolderGuide { get; private set; }

        [Inject]
        private void Inject(StringDistributor distributor, ColoredStringHolderStash stash)
        {
            _distributor = distributor;
            _coloredStringHolderStash = stash;
        }

        public void SetWhiteStringHolderGuide(bool isWhiteStringHolderGuide)
        {
            IsWhiteStringHolderGuide = isWhiteStringHolderGuide;
        }

        protected override void ValidateHit(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out Bolt bolt) == false)
                return;

            if (bolt.IsAnimating)
                return;

            if (_coloredStringHolderStash.TryGetColoredStringHolder(bolt.Colorable.Color, out ColoredStringHolder holder) == IsWhiteStringHolderGuide)
                return;

            _distributor.Distribute(bolt);
            bolt.Unscrew();
            Unscrewed?.Invoke();
        }
    }
}