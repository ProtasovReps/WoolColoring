using UISystem.Buttons;
using UnityEngine;
using YG;

namespace Yandex.Inaps
{
    public class InapBuyButton : ButtonView
    {
        [SerializeField] private PurchaseYG _purchaseYG;

        protected override void OnButtonClick()
        {
            _purchaseYG.BuyPurchase();
            base.OnButtonClick();
        }
    }
}
