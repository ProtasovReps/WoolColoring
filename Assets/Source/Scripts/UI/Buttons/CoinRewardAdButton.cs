using Reflex.Attributes;
using UnityEngine;
using YG;

public class CoinRewardAdButton : CoinRewardButton
{
    [SerializeField] private float _firstRechargeTime = 0f;

    private readonly string _id = RewardIds.Coin;

    [Inject]
    private void Inject() => SetReload(_firstRechargeTime);

    protected override void ProcessReward()
    {
        YG2.RewardedAdvShow(_id, OnAdWatched);
    }

    private void OnAdWatched()
    {
        base.ProcessReward();
    }
}