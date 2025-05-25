using System.Collections.Generic;
using UnityEngine;
using Reflex.Attributes;

namespace Effects
{
    public class EffectPool : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _effectPrefab;
        [SerializeField] private EffectPlayer _effectPlayer;

        private Transform _transform;
        private Queue<ParticleSystem> _freeEffects;

        [Inject]
        private void Inject()
        {
            _freeEffects = new Queue<ParticleSystem>();
            _transform = transform;
            _effectPlayer.EffectCompleted += OnEffectCompleted;
        }

        public ParticleSystem Get()
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

            return effect;
        }

        private void OnEffectCompleted(ParticleSystem effect)
        {
            _freeEffects.Enqueue(effect);
        }
    }
}