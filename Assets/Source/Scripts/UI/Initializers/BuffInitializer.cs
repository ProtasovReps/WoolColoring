using Reflex.Attributes;
using UnityEngine;

public class BuffInitializer : MonoBehaviour
{
    [SerializeField] private BuffButton _unlocker;
    [SerializeField] private BuffButton _filler;
    [SerializeField] private BuffButton _exploder;
    [SerializeField] private BuffButton _cleaner;

    [Inject]
    private void Inject(Unlocker unlockStrategy, Filler fillStrategy,
    Breaker explodeStrategy, Remover clearStrategy)
    {
        _unlocker.Initialize(unlockStrategy);
        _filler.Initialize(fillStrategy);
        _exploder.Initialize(explodeStrategy);
        _cleaner.Initialize(clearStrategy);
    }
}