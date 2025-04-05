using UnityEngine;

public class BuffInitializer : MonoBehaviour
{
    [SerializeField] private BuffButton _unlocker;
    [SerializeField] private BuffButton _filler;
    [SerializeField] private BuffButton _exploder;
    [SerializeField] private BuffButton _cleaner;
    [SerializeField] private UnlockerBuyButton _unlockerBuyButton;
    [SerializeField] private FillerBuyButton _fillerBuyButton;
    [SerializeField] private BreakerBuyButton _breakerBuyButton;
    [SerializeField] private RemoverBuyButton _removerBuyButton;

    public void Initialize(Unlocker unlockStrategy, Filler fillStrategy,
    Breaker explodeStrategy, Remover clearStrategy)
    {
        _unlocker.Initialize(unlockStrategy);
        _filler.Initialize(fillStrategy);
        _exploder.Initialize(explodeStrategy);
        _cleaner.Initialize(clearStrategy);
        _unlockerBuyButton.Initialize(unlockStrategy);
        _fillerBuyButton.Initialize(fillStrategy);
        _breakerBuyButton.Initialize(explodeStrategy);
        _removerBuyButton.Initialize(clearStrategy);
    }
}