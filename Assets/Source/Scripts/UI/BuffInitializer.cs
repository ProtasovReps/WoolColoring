using UnityEngine;

public class BuffInitializer : MonoBehaviour
{
    [SerializeField] private BuffButton _unlocker;
    [SerializeField] private BuffButton _filler;
    [SerializeField] private BuffButton _exploder;
    [SerializeField] private BuffButton _cleaner;
    [SerializeField] private BuyBuffButton _unlockerBuyButton;
    [SerializeField] private BuyBuffButton _fillerBuyButton;
    [SerializeField] private BuyBuffButton _exploderBuyButton;
    [SerializeField] private BuyBuffButton _cleanerBuyButton;

    public void Initialize(UnlockHolderStrategy unlockStrategy, FillHolderStrategy fillStrategy,
    ExplodeFigureStrategy explodeStrategy, ClearWhiteHolderStrategy clearStrategy)
    {
        _unlocker.Initialize(unlockStrategy);
        _filler.Initialize(fillStrategy);
        _exploder.Initialize(explodeStrategy);
        _cleaner.Initialize(clearStrategy);
        _unlockerBuyButton.Initialize(unlockStrategy);
        _fillerBuyButton.Initialize(fillStrategy);
        _exploderBuyButton.Initialize(explodeStrategy);
        _cleanerBuyButton.Initialize(clearStrategy);
    }
}