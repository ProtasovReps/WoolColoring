using YG;

public class StarterPackPurchase : PurchaseValidation
{
    private const int CoinsAddAmount = 1500;
    private const int BuffsAddAmount = 5;

    private readonly InapCoinsAdder _coinAdder;
    private readonly InapBuffAdder _buffAdder;
    private readonly AdsRemover _adsRemover;

    public StarterPackPurchase(InapCoinsAdder coinAdder, InapBuffAdder buffAdder, AdsRemover adsRemover, ProgressSaver progressSaver) : base(progressSaver)
    {
        _coinAdder = coinAdder;
        _buffAdder = buffAdder;
        _adsRemover = adsRemover;
    }

    protected override void Validate(string purchaseId)
    {
        if (purchaseId != PurchaseIds.StarterPack)
            return;

        _adsRemover.RemoveAds();
        _coinAdder.AddCoins(CoinsAddAmount);
        _buffAdder.AddBuffs(BuffsAddAmount);
    }
}