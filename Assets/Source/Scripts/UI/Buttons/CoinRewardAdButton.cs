using YG;

public class CoinRewardAdButton : CoinRewardButton
{
    private readonly string _id = RewardIds.Coin;

    protected override void ProcessReward()
    {
        YG2.RewardedAdvShow(_id, OnAdWatched);
    }

    private void OnAdWatched()
    {
        base.ProcessReward();
    }
}