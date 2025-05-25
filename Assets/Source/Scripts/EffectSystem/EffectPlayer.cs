using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Effects
{
    public class EffectPlayer : MonoBehaviour
    {
        public event Action<ParticleSystem> EffectCompleted;

        public async UniTaskVoid Play(ParticleSystem effect)
        {
            effect.gameObject.SetActive(true);
            effect.Play();

            await UniTask.WaitForSeconds(effect.main.duration);
            EffectCompleted?.Invoke(effect);
            effect.gameObject.SetActive(false);
        }
    }
}