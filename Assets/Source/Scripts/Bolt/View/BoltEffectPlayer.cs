using Reflex.Attributes;
using System.Collections.Generic;
using UnityEngine;

public class BoltEffectPlayer : MonoBehaviour
{
    [SerializeField] private EffectPool _effectPool;
    [SerializeField] private EffectPlayer _effectPlayer;

    private BoltStash _boltStash;

    [Inject]
    private void Inject(BoltStash boltStash)
    {
        _boltStash = boltStash;
        _boltStash.BoltsAdded += SubscribeBolts;
    }

    private void OnDestroy()
    {
        foreach (Bolt bolt in _boltStash.Bolts)
            bolt.Disabling -= OnBoltDisabling;
    }

    private void SubscribeBolts(IEnumerable<Bolt> bolts)
    {
        foreach (Bolt bolt in bolts)
            bolt.Disabling += OnBoltDisabling;
    }

    private void OnBoltDisabling(Bolt boltView)
    {
        ParticleSystem effect = _effectPool.Get();
        effect.transform.position = boltView.Transform.position;
        _effectPlayer.Play(effect).Forget();
    }
}