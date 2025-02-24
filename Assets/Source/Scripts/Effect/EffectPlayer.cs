using System.Collections;
using UnityEngine;
using System;

public class EffectPlayer : MonoBehaviour
{
    public event Action<ParticleSystem> EffectCompleted;

    public void Play(ParticleSystem effect, WaitForSeconds effectDuration)
        => StartCoroutine(PlayEffect(effect, effectDuration));

    private IEnumerator PlayEffect(ParticleSystem effect, WaitForSeconds effectDuration)
    {
        effect.gameObject.SetActive(true);
        effect.Play();

        yield return effectDuration;
        EffectCompleted?.Invoke(effect);
        effect.gameObject.SetActive(false);
    }
}