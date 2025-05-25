using PlayerWallet;
using Reflex.Attributes;
using UnityEngine;
using YG;

namespace PlayerGuide
{
    public class MoneyAddReplic : DefaultReplic
    {
        [SerializeField] private int _addAmount;

        private Wallet _wallet;

        [Inject]
        private void Inject(Wallet wallet)
        {
            _wallet = wallet;
        }

        protected override void OnAnimationFinalized()
        {
            if (YG2.saves.IfFreeCoinsGiven == false)
                _wallet.Add(_addAmount);

            YG2.saves.IfFreeCoinsGiven = true;

            base.OnAnimationFinalized();
        }
    }
}