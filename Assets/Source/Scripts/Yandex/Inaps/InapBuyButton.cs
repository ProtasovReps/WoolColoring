using UnityEngine;
using YG;

public class InapBuyButton : ButtonView
{
    [SerializeField] private PurchaseYG _purchaseYG;

    protected override void OnButtonClick()
    {
        _purchaseYG.BuyPurchase();
        base.OnButtonClick();
    }
}
