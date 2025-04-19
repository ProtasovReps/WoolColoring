using Reflex.Attributes;

public class CoinRewardButton : RewardButton
{
    private Wallet _wallet;

    [Inject]
    private void Inject(Wallet wallet)
       => _wallet = wallet;

    protected override void ProcessReward()
        => _wallet.Add(RewardAmount);
}