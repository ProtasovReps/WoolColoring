using UnityEngine;

public class BuffRewardButton : RechargeableButton
{
    [SerializeField] private BuffDealMenu _dealMenu;

    protected override void OnButtonClick()
    {
        _dealMenu.ShowAd();
        base.OnButtonClick();
    }
}