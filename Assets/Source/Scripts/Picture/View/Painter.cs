using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Reflex.Attributes;
using BlockPicture.Model;
using StringHolders.Model;
using CustomInterface;

namespace BlockPicture.View
{
    public class Painter : MonoBehaviour
    {
        [SerializeField] private float _colorizeDelay;
        [SerializeField, Min(1)] private int _blocksPerString;

        private Picture _picture;
        private ColoredStringHolderStash _holderStash;
        private ColoredStringHolderSwitcher _switcher;

        [Inject]
        private void Inject(Picture picture)
        {
            _picture = picture;

            Subscribe(_holderStash.ActiveHolders);
            Subscribe(_holderStash.InactiveHolders);
        }

        private void OnDestroy()
        {
            Unsubscribe(_holderStash.ActiveHolders);
            Unsubscribe(_holderStash.InactiveHolders);
        }

        public void Initialize(ColoredStringHolderStash holderStash, ColoredStringHolderSwitcher switcher)
        {
            if (holderStash == null)
                throw new ArgumentNullException(nameof(holderStash));

            if (switcher == null)
                throw new ArgumentNullException(nameof(switcher));

            _holderStash = holderStash;
            _switcher = switcher;
        }

        private void OnHolderFilled(ColoredStringHolder holder)
        {
            FillImage(holder).Forget();
        }

        private async UniTaskVoid FillImage(ColoredStringHolder holder)
        {
            Color color = holder.Color;

            holder.SetEnabled(false);

            for (int i = 0; i < holder.MaxStringCount; i++)
            {
                holder.GetLastString();

                for (int j = 0; j < _blocksPerString; j++)
                {
                    _picture.Colorize(color);
                    await UniTask.WaitForSeconds(_colorizeDelay);
                }
            }

            holder.SetEnabled(true);
            _switcher.Switch(holder);
        }

        private void Subscribe(IEnumerable<IFillable<ColoredStringHolder>> holders)
        {
            foreach (IFillable<ColoredStringHolder> holder in holders)
                holder.Filled += OnHolderFilled;
        }

        private void Unsubscribe(IEnumerable<IFillable<ColoredStringHolder>> holders)
        {
            foreach (IFillable<ColoredStringHolder> holder in holders)
                holder.Filled -= OnHolderFilled;
        }
    }
}