using Ami.BroAudio;
using Coffee.UIExtensions;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

public class CountChangeableEffect : TemporaryActivatableUI
{
    private const string FirstSymbol = "+";

    [SerializeField] private UIParticle _effect;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private SoundID _effectSound;

    private ICountChangeable _countChangeable;
    private int _lastCount;

    [Inject]
    private void Inject(ICountChangeable countChangeable)
    {
        _countChangeable = countChangeable;
        _countChangeable.CountChanged += OnCountChanged;

        _lastCount = _countChangeable.Count;
    }

    private void OnCountChanged()
    {
        int addedAmount = _countChangeable.Count - _lastCount;

        _lastCount = _countChangeable.Count;

        if (addedAmount < 0)
            return;

        _text.text = $"{FirstSymbol}{addedAmount}";

        Activate();
        _effect.Play();
        BroAudio.Play(_effectSound);
    }
}