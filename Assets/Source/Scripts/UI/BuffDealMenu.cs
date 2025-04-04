using Reflex.Attributes;
using System;
using YG;

public class BuffDealMenu : ActivatableUI
{
    private const int AddAmount = 1;

    private (string, IBuff) _targetReward;
    private BuffBag _bag;
    private bool _isTargetRewardSetted;

    public void SetTargetReward(string rewardId, IBuff buff)
    {
        if (string.IsNullOrEmpty(rewardId))
            throw new ArgumentNullException(nameof(rewardId));

        if (buff == null)
            throw new ArgumentNullException(nameof(buff));

        _targetReward.Item1 = rewardId;
        _targetReward.Item2 = buff;
        _isTargetRewardSetted = true;
    }

    public void ShowAd()
    {
        YG2.RewardedAdvShow(_targetReward.Item1, GetBuff);
    }

    private void GetBuff()
    {
        if (_isTargetRewardSetted == false)
            throw new InvalidOperationException(nameof(_isTargetRewardSetted));

        _bag.AddBuff(_targetReward.Item2, AddAmount);

        _isTargetRewardSetted = false;
    }

    [Inject]
    private void Inject(BuffBag buffBag)
    {
        _bag = buffBag;
    }
}