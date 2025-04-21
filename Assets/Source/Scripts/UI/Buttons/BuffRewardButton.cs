using UnityEngine;

public class BuffRewardButton : RechargeableButton
{
    [SerializeField] private BuffDealMenu _dealMenu;
    [SerializeField] private int _buffRewardCount = 1;

    protected override void OnButtonClick()
    {
        _dealMenu.AddReward(_buffRewardCount);
        base.OnButtonClick();
    }
}