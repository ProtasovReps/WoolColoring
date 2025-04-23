using Reflex.Attributes;
using UnityEngine;
using YG;

public class AcceptGameLabelButton : ButtonView
{
    [SerializeField] private GameLabelYG _gameLabelYG;
    [SerializeField, Min(0)] private int _addAmount;

    private Wallet _wallet;

    [Inject]
    private void Inject(Wallet wallet)
    {
        _wallet = wallet;
    }

    protected override void OnButtonClick()
    {
        _wallet.Add(_addAmount);
        _gameLabelYG.GameLabelShowDialog();
        base.OnButtonClick();
    }
}