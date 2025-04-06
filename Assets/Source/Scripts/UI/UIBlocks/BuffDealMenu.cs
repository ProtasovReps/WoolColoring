using Reflex.Attributes;
using System;
using UnityEngine;
using YG;

public class BuffDealMenu : ActivatableUI
{
    private const int AddAmount = 1;

    [SerializeField] private Store _store;

    private Wallet _wallet;
    private IBuff _targetBuffReward;

    public void SetTargetReward(IBuff buff)
    {
        if (buff == null)
            throw new ArgumentNullException(nameof(buff));

        _targetBuffReward = buff;
    }

    public void ShowAd()
    {
        YG2.RewardedAdvShow(_targetBuffReward.Id, AddBuff);
    }

    [Inject]
    private void Inject(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void AddBuff()
    {
        if (_targetBuffReward == null)
            throw new ArgumentNullException(nameof(_targetBuffReward));

        _wallet.Add(_targetBuffReward.Price);
        _store.Purchase(_targetBuffReward, AddAmount);

        _targetBuffReward = null;
    }
}