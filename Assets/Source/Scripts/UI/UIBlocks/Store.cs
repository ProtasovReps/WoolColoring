using Ami.BroAudio;
using Reflex.Attributes;
using UnityEngine;

public class Store : ActivatableUI
{
    [SerializeField] private SoundID _mainMusic;
    [SerializeField] private SoundID _storeMusic;
    [SerializeField] private TemporaryActivatableUI _notEnoughMoneyText;

    private Wallet _wallet;
    private BuffBag _bag;

    [Inject]
    private void Inject(Wallet wallet, BuffBag buffBag)
    {
        _wallet = wallet;
        _bag = buffBag;
    }

    public override void Activate()
    {
        if (IsAnimating)
            return;

        BroAudio.Pause(_mainMusic);
        BroAudio.Play(_storeMusic);
        base.Activate();
    }

    public override void Deactivate()
    {
        if (IsAnimating)
            return;

        BroAudio.Pause(_storeMusic);
        BroAudio.Play(_mainMusic);
        base.Deactivate();
    }

    public bool TryPurchase(IBuff buff, int count)
    {
        if (_wallet.TrySpend(buff.Price * count) == false)
        {
            _notEnoughMoneyText.Activate();
            return false;
        }

        _bag.AddBuff(buff, count);
        return true;
    }
}