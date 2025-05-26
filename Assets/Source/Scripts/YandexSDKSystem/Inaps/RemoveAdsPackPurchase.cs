using YG;
using Extensions;
using YandexGamesSDK.Saves;

namespace YandexGamesSDK.Inaps
{
    public class RemoveAdsPackPurchase : PurchaseValidation
    {
        private readonly AdsRemover _adsRemover;

        public RemoveAdsPackPurchase(ProgressSaver progressSaver, AdsRemover adsRemover)
            : base(progressSaver)
        {
            _adsRemover = adsRemover;
        }

        protected override void Validate(string purchaseId)
        {
            if (purchaseId != PurchaseIds.AdsRemove)
                return;

            _adsRemover.RemoveAds();

            YG2.MetricaSend(MetricParams.InappBought.ToString());
        }
    }
}