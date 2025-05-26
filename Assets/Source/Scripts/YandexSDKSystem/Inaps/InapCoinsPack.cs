using YandexGamesSDK.Saves;

namespace YandexGamesSDK.Inaps
{
    public class InapCoinsPack : PurchaseValidation
    {
        private readonly InapCoinsAdder _coinsAdder;

        public InapCoinsPack(ProgressSaver progressSaver, InapCoinsAdder coinsAdder)
            : base(progressSaver)
        {
            _coinsAdder = coinsAdder;
        }

        protected override void Validate(string purchaseId)
        {
            if (purchaseId == PurchaseIds.SmallCoins)
                _coinsAdder.AddCoins(CoinsAddValues.SmallPackage);

            if (purchaseId == PurchaseIds.MediumCoins)
                _coinsAdder.AddCoins(CoinsAddValues.MediumPackage);

            if (purchaseId == PurchaseIds.LargeCoins)
                _coinsAdder.AddCoins(CoinsAddValues.LargePackage);
        }
    }
}