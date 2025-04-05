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

    public void SetTargetReward(IBuff buff)
    {
        if (buff == null)
            throw new ArgumentNullException(nameof(buff));

        _targetReward = buff;
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
        if (_targetReward == null)
            throw new ArgumentNullException(nameof(_targetReward));

        _wallet.Add(_targetReward.Price);
        _store.Purchase(_targetReward, AddAmount);

        _targetReward = null;
    }
}