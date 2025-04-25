using UnityEngine;

public abstract class RewardButton : RechargeableButton
{
    [SerializeField] private int _rewardAmount;

    protected int RewardAmount => _rewardAmount;

    protected override void OnButtonClick()
    {
        Debug.Log("Меня нажали!");
        ProcessReward();
        base.OnButtonClick();
    }

    protected abstract void ProcessReward();
}