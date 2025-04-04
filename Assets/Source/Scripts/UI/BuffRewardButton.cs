using UnityEngine;

public class BuffRewardButton : ButtonView
{
    [SerializeField] private BuffDealMenu _dealMenu;

    protected override void OnButtonClick()
    {
        _dealMenu.ShowAd();
        base.OnButtonClick();
    }
}