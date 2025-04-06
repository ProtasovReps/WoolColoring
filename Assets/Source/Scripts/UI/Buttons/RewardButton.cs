using UnityEngine;

public abstract class RewardButton : RechargeableButton
{
    [SerializeField] private int _rewardAmount;

    protected int RewardAmount => _rewardAmount;

    protected override void OnButtonClick()
    {
        base.OnButtonClick();
        ProcessReward();
    }

    protected abstract void ProcessReward();
}