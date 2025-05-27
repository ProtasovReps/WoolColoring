using Extensions;
using WalletSystem;
using Reflex.Attributes;
using UnityEngine;
using YG;

namespace UISystem.Buttons
{
    public class CoinAdRewardButton : CoinTimerRechargeableButton
    {
        private readonly string _id = RewardIds.Coin;

        [SerializeField] private int _rewardAmount;

        private Wallet _wallet;

        [Inject]
        private void Inject(Wallet wallet)
        {
            _wallet = wallet;
        }

        protected override void OnButtonClick()
        {
            ProcessReward();
            base.OnButtonClick();
        }

        private void ProcessReward()
        {
            YG2.RewardedAdvShow(_id, AddCoins);
        }

        private void AddCoins()
        {
            _wallet.Add(_rewardAmount);
        }
    }
}