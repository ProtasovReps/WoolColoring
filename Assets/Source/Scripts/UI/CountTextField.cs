using Reflex.Attributes;
using TMPro;
using UnityEngine;

public class CountTextField : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private ICountChangeable _countChangeable;

    [Inject]
    private void Inject(ICountChangeable countChangeable)
    {
        _countChangeable = countChangeable;
        OnCountChanged();
        _countChangeable.CountChanged += OnCountChanged;
    }

    private void OnDestroy()
    {
        _countChangeable.CountChanged -= OnCountChanged;
    }

    private void OnCountChanged()
    {
        _text.text = _countChangeable.Count.ToString();
    }
}