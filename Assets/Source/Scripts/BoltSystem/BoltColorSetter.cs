using System;
using UnityEngine;
using Bolts.View;
using BlockPicture.Model;

namespace Bolts
{
    public class BoltColorSetter : IDisposable
    {
        private readonly BoltStash _boltStash;
        private readonly Picture _picture;

        public BoltColorSetter(BoltStash stash, Picture picture)
        {
            if (stash == null)
                throw new ArgumentNullException(nameof(stash));

            if (picture == null)
                throw new ArgumentNullException(nameof(picture));

            _boltStash = stash;
            _picture = picture;
            _picture.Filled += OnColorFilled;
        }

        public void Dispose()
        {
            _picture.Filled -= OnColorFilled;
        }

        public void SetColors()
        {
            foreach (Bolt bolt in _boltStash.Bolts)
            {
                if (_picture.GetRandomColor(out Color color) == false)
                {
                    return;
                }

                bolt.ColorSettable.SetColor(color);
            }
        }

        private void OnColorFilled(Color color)
        {
            foreach (Bolt bolt in _boltStash.Bolts)
            {
                if (bolt.Colorable.Color == color)
                {
                    if (_picture.GetRandomColor(out Color newColor) == false)
                    {
                        return;
                    }

                    bolt.ColorSettable.SetColor(newColor);
                }
            }
        }
    }
}