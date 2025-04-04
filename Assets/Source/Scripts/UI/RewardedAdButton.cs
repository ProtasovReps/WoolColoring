using UnityEngine;
using YG;

public abstract class RewardedAdButton : CounterButton
{
    private string _rewardId;

    public void Initialize(string id) => _rewardId = id;

    protected override void OnButtonClick()
    {
        YG2.RewardedAdvShow(_rewardId, ReceiveReward);
        base.OnButtonClick();
    }

    protected abstract void ReceiveReward();
}