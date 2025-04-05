using Reflex.Attributes;
using System;
using UnityEngine;
using YG;

public class BuffDealMenu : ActivatableUI
{
    private const int AddAmount = 1;

    [SerializeField] private Store _store;

    private Wallet _wallet;
    private IBuff _targetReward;
    private bool _isTargetRewardSetted;

    public void SetTargetReward(IBuff buff)
    {
        if (buff == null)
            throw new ArgumentNullException(nameof(buff));

        _targetReward = buff;
        _isTargetRewardSetted = true;
    }

    public void ShowAd()
    {
        YG2.RewardedAdvShow(_targetReward.Id, AddBuff);
    }

    [Inject]
    private void Inject(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void AddBuff()
    {
        if (_isTargetRewardSetted == false)
            throw new InvalidOperationException(nameof(_isTargetRewardSetted));

        _wallet.Add(_targetReward.Price);
        _store.Purchase(_targetReward, AddAmount);

        _isTargetRewardSetted = false;
    }
}