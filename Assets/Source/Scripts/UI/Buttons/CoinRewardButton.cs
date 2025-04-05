using Reflex.Attributes;
using UnityEngine;

public class CoinRewardButton : RewardedAdButton
{
    [SerializeField] private int _rewardAmount;

    private Wallet _wallet;

    private void Awake() => Initialize(RewardIds.Coin);

    protected override void ReceiveReward()
       => _wallet.Add(_rewardAmount);

    [Inject]
    private void Inject(Wallet wallet) => _wallet = wallet;
}
