using UnityEngine;

public class BuffInitializer : MonoBehaviour
{
    [SerializeField] private BuffButton _unlocker;
    [SerializeField] private BuffButton _filler;
    [SerializeField] private BuffButton _breaker;
    [SerializeField] private BuffButton _cleaner;

    public void Initialize(UnlockHolderStrategy unlockStrategy) //проинициализироватиь и оставшиеся баффы
    {
        _unlocker.Initialize(unlockStrategy);
    }
}
