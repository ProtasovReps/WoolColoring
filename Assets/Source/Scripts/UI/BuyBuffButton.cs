using Ami.BroAudio;
using Reflex.Attributes;
using System;
using TMPro;
using UnityEngine;

public class BuyBuffButton : ButtonView
{
    [SerializeField] private SoundID _notEnoughMoneySound;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private TemporaryActivatableUI _notEnoughMoneyText;
    [SerializeField, Min(1)] private int _buffAddCount;

    private Wallet _wallet;
    private BuffBag _bag;
    private IBuff _buff;

    public void Initialize(IBuff buff)
    {
        if(buff == null)
            throw new ArgumentNullException(nameof(buff));

        _buff = buff;
        _priceText.text = _buff.Price.ToString();
    }

    [Inject]
    private void Inject(Wallet wallet, BuffBag bag)
    {
        _wallet = wallet;
        _bag = bag;
    }

    protected override void OnButtonClick()
    {
        if(_wallet.TrySpend(_buff.Price) == false)
        {
            BroAudio.Play(_notEnoughMoneySound);
            _notEnoughMoneyText.Activate();
            return;
        }

        _bag.AddBuff(_buff, _buffAddCount);
        base.OnButtonClick();
    }
}