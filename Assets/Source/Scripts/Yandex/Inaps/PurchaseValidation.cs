using System;
using YG;
using Extensions;
using YandexGamesSDK.Saves;

namespace YandexGamesSDK.Inaps
{
    public abstract class PurchaseValidation : IDisposable
    {
        private readonly ProgressSaver _progressSaver;

        public PurchaseValidation(ProgressSaver progressSaver)
        {
            YG2.onPurchaseSuccess += OnPurchaseSuccess;
            _progressSaver = progressSaver;
        }

        public void Dispose()
        {
            YG2.onPurchaseSuccess -= OnPurchaseSuccess;
        }

        protected abstract void Validate(string purchaseId);

        private void OnPurchaseSuccess(string purchaseId)
        {
            Validate(purchaseId);
            _progressSaver.Save();
            YG2.MetricaSend(MetricParams.InappBought.ToString());
        }
    }
}