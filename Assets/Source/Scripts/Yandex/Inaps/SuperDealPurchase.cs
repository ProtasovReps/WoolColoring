using YandexGamesSDK.Saves;

namespace YandexGamesSDK.Inaps
{
    public class SuperDealPurchase : PurchaseValidation
    {
        private const int BuffsAddCount = 10;

        private readonly InapBuffAdder _buffAdder;

        public SuperDealPurchase(ProgressSaver progressSaver, InapBuffAdder buffAdder)
            : base(progressSaver)
        {
            _buffAdder = buffAdder;
        }

        protected override void Validate(string purchaseId)
        {
            if (purchaseId != PurchaseIds.SuperDeal)
                return;

            _buffAdder.AddBuffs(BuffsAddCount);
        }
    }
}