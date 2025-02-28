using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectPool : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effectPrefab;
    [SerializeField] private EffectPlayer _effectPlayer;

    private Transform _transform;
    private BoltStash _boltStash;
    private Queue<ParticleSystem> _freeEffects;

    private void OnDisable()
    {
        foreach (Bolt bolt in _boltStash.Bolts)
            bolt.Disabling -= OnBoltDisabling;
    }

    public void Initialize(BoltStash boltStash)
    {
        if (boltStash == null)
            throw new ArgumentNullException(nameof(boltStash));

        _transform = transform;
        _boltStash = boltStash;
        _freeEffects = new Queue<ParticleSystem>();

        _effectPlayer.EffectCompleted += OnEffectCompleted;
        _boltStash.BoltsAdded += SubscribeBolts;
    }

    private void SubscribeBolts(IEnumerable<Bolt> bolts)
    {
        foreach (Bolt bolt in bolts)
            bolt.Disabling += OnBoltDisabling;
    }

    private void OnBoltDisabling(Bolt boltView)
    {
        ParticleSystem effect;

        if (_freeEffects.Count == 0)
        {
            effect = Instantiate(_effectPrefab);
            effect.transform.SetParent(_transform);
        }
        else
        {
            effect = _freeEffects.Dequeue();
        }

        effect.transform.position = boltView.Transform.position;
        _effectPlayer.Play(effect);
    }

    private void OnEffectCompleted(ParticleSystem effect)
        => _freeEffects.Enqueue(effect);
}