using Reflex.Attributes;
using System;
using UnityEngine;
using YG;

public class BuffDealMenu : ActivatableUI
{
    [SerializeField] private UnlockerBuyButton _unlockerBuyButton;
    [SerializeField] private RemoverBuyButton _removerBuyButton;
    [SerializeField] private BreakerBuyButton _breakerBuyButton;
    [SerializeField] private FillerBuyButton _fillerBuyButton;

    private IBuffBuyButton<IBuff>[] _buyBuffButtons;
    private Store _store;
    private Wallet _wallet;
    private IBuff _targetBuffReward;

    [Inject]
    private void Inject(Wallet wallet, Store store)
    {
        _wallet = wallet;
        _store = store;
        _buyBuffButtons = new IBuffBuyButton<IBuff>[] { _unlockerBuyButton, _removerBuyButton, _breakerBuyButton, _fillerBuyButton };
    }

    public override void Activate()
    {
        for (int i = 0; i < _buyBuffButtons.Length; i++)
            _buyBuffButtons[i].SetActive(_buyBuffButtons[i].CurrentBuff == _targetBuffReward);

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

        _wallet.AddSilent(_targetBuffReward.Price * count);
        _store.TryPurchase(_targetBuffReward, count);

        _targetBuffReward = null;
    }
}