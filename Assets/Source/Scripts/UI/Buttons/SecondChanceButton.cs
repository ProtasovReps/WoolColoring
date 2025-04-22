using Reflex.Attributes;
using UnityEngine;
using YG;

public class SecondChanceButton : ButtonView
{
    [SerializeField] private ActivatableUI _block;

    private WhiteStringHolder _whiteHolder;

    [Inject]
    private void Inject(WhiteStringHolder whiteHolder)
    {
        _whiteHolder = whiteHolder;
    }

    protected override void OnButtonClick()
    {
        YG2.RewardedAdvShow(RewardIds.SecondChance, OnAdWatched);
        base.OnButtonClick();
    }

    private void OnAdWatched()
    {
        YG2.MetricaSend(MetricParams.RevivedWithAd.ToString());
        _block.Deactivate();
        Deactivate();
        _whiteHolder.RemoveAllStrings();
    }
}