using System;
using UnityEngine;
using BlockPicture.Model;

namespace StringHolders.Model
{
    public class ExtraStringRemover : IDisposable
    {
        private readonly Picture _picture;
        private readonly WhiteStringHolder _whiteStringHolder;

        public ExtraStringRemover(Picture picture, WhiteStringHolder holder)
        {
            if (picture == null)
                throw new ArgumentNullException(nameof(picture));

            if (holder == null)
                throw new ArgumentNullException(nameof(holder));

            _picture = picture;
            _whiteStringHolder = holder;
            _picture.Filled += OnColorFilled;
        }

        public void Dispose()
        {
            _picture.Filled -= OnColorFilled;
        }

        private void OnColorFilled(Color color)
        {
            int requiredStringsCount = _whiteStringHolder.GetColorCount(color);

            if (requiredStringsCount == 0)
                return;

            for (int i = 0; i < requiredStringsCount; i++)
                _whiteStringHolder.GetColorable(color);
        }
    }
}