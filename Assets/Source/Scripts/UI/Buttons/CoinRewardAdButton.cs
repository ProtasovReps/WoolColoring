using Reflex.Attributes;
using YG;

public class CoinRewardAdButton : RewardButton
{
    private readonly string _id = RewardIds.Coin;
    private Wallet _wallet;

    [Inject]
    private void Inject(Wallet wallet)
       => _wallet = wallet;

    protected override void ProcessReward()
    {
        YG2.RewardedAdvShow(_id, AddCoins);
    }

    private void AddCoins()
    {
        _wallet.Add(RewardAmount);
    }
}