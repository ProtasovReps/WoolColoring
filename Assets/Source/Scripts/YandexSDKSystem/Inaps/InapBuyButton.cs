using LevelInterface.Buttons;
using UnityEngine;
using YG;

namespace YandexGamesSDK.Inaps
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
