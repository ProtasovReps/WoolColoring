using Ami.BroAudio;
using Reflex.Attributes;
using UnityEngine;

public class CountChangeableEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;
    [SerializeField] private SoundID _effectSound;

    private ICountChangeable _countChangeable;

    [Inject]
    private void Inject(ICountChangeable countChangeable)
    {
        _countChangeable = countChangeable;
        _countChangeable.CountChanged += OnCountChanged;
    }

    private void OnCountChanged()
    {
        _effect.Play();
        BroAudio.Play(_effectSound);
    }
}