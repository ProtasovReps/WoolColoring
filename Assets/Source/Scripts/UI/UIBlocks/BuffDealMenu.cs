using Reflex.Attributes;
using System;
using UnityEngine;
using YG;

public class BuffDealMenu : ActivatableUI
{
    private const int AddAmount = 1;

    [SerializeField] private BuyBuffButton[] _buyBuffButtons;

    private Store _store;
    private Wallet _wallet;
    private IBuff _targetBuffReward;

    [Inject]
    private void Inject(Wallet wallet, Store store)
    {
        _wallet = wallet;
        _store = store;
    }

    public override void Activate()
    {
        for (int i = 0; i < _buyBuffButtons.Length; i++)
            _buyBuffButtons[i].gameObject.SetActive(_buyBuffButtons[i].CurrentBuff == _targetBuffReward);

        base.Activate();
    }

    public void SetTargetReward(IBuff buff)
    {
        if (buff == null)
            throw new ArgumentNullException(nameof(buff));

        _targetBuffReward = buff;
    }

    public void AddReward(int count)
    {
        YG2.RewardedAdvShow(_targetBuffReward.Id, () => AddBuff(count));
    }

    private void AddBuff(int count)
    {
        if (_targetBuffReward == null)
            throw new ArgumentNullException(nameof(_targetBuffReward));

        for (int i = 0; i < count; i++)
        {
            _wallet.AddSilent(_targetBuffReward.Price);
            _store.TryPurchase(_targetBuffReward, AddAmount);
        }

        _targetBuffReward = null;
    }
}