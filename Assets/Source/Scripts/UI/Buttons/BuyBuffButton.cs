using Ami.BroAudio;
using Reflex.Attributes;
using System;
using TMPro;
using UnityEngine;

public abstract class BuyBuffButton : ButtonView
{
    [SerializeField] private SoundID _purchaseRejectedSound;
    [SerializeField] private ParticleSystem _purchaseEffect;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField, Min(1)] private int _buffBuyCount;

    private Store _store;
    private IBuff _buff;

    public IBuff CurrentBuff => _buff;

    [Inject]
    private void Inject(Store store)
    {
        _store = store;
    }

    public void Initialize(IBuff buff)
    {
        if (buff == null)
            throw new ArgumentNullException(nameof(buff));

        _buff = buff;
        _priceText.text = _buff.Price.ToString();
    }

    protected override void OnButtonClick()
    {
        if (_store.TryPurchase(_buff, _buffBuyCount) == false)
        {
            BroAudio.Play(_purchaseRejectedSound);
        }
        else
        {
            _purchaseEffect.Play();
            base.OnButtonClick();
        }
    }
}