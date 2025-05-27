using MolbertSystem.Model;
using FigurePlatformSystem;
using FigurePlatformSystem.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace WalletSystem
{
    public class MoneyRewards : IDisposable
    {
        private readonly Picture _picture;
        private readonly Wallet _wallet;
        private readonly FigureCompositionFactory _compositionFactory;
        private readonly List<FigureComposition> _compositions;

        public MoneyRewards(Picture picture, Wallet wallet, FigureCompositionFactory compositionFactory)
        {
            if (picture == null)
                throw new ArgumentNullException(nameof(picture));

            if (wallet == null)
                throw new ArgumentNullException(nameof(wallet));

            if (compositionFactory == null)
                throw new ArgumentNullException(nameof(compositionFactory));

            _picture = picture;
            _wallet = wallet;
            _compositionFactory = compositionFactory;
            _compositions = new List<FigureComposition>();
            _picture.Filled += OnColorFilled;
            _picture.Finished += OnPictureColorized;
            _compositionFactory.Produced += OnCompositionProduced;
        }

        public void Dispose()
        {
            _picture.Filled -= OnColorFilled;
            _picture.Finished -= OnPictureColorized;
            _compositionFactory.Produced -= OnCompositionProduced;

            for (int i = 0; i < _compositions.Count; i++)
                _compositions[i].Emptied -= OnCompositionEmptied;
        }

        private void OnCompositionProduced(FigureComposition composition)
        {
            composition.Emptied += OnCompositionEmptied;
            _compositions.Add(composition);
        }

        private void OnColorFilled(Color color)
        {
            _wallet.Add(RewardValues.ColorFilledReward);
        }

        private void OnPictureColorized()
        {
            _wallet.Add(RewardValues.PictureColorizedReward);
        }

        private void OnCompositionEmptied(FigureComposition composition)
        {
            _wallet.Add(RewardValues.CompositionFalledReward);
        }
    }
}