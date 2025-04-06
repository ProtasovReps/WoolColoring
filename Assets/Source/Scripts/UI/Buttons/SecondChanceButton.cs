using Reflex.Attributes;
using YG;

public class SecondChanceButton : ButtonView
{
    private WhiteStringHolder _whiteHolder;

    protected override void OnButtonClick()
    {
        YG2.RewardedAdvShow(RewardIds.SecondChance, OnAdWatched);
        Deactivate();
        base.OnButtonClick();
    }

    private void OnAdWatched()
    {
        _whiteHolder.RemoveAllStrings();
    }

    [Inject]
    private void Inject(WhiteStringHolder whiteHolder)
    {
        _whiteHolder = whiteHolder;
    }
}