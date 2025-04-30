using Reflex.Attributes;
using UnityEngine;
using YG;

public class CoinAdRewardButton : CoinTimerRechargeableButton
{
    [SerializeField] private int _rewardAmount;

    private readonly string _id = RewardIds.Coin;
    private Wallet _wallet;

    [Inject]
    private void Inject(Wallet wallet) => _wallet = wallet;

    protected override void OnButtonClick()
    {
        ProcessReward();
        base.OnButtonClick();
    }

    private void ProcessReward() => YG2.RewardedAdvShow(_id, AddCoins);

    private void AddCoins() => _wallet.Add(_rewardAmount);
}