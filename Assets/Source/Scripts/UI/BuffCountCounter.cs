using Reflex.Attributes;
using System;
using TMPro;
using UnityEngine;

public class BuffCountCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private BuffBag _bag;
    private IBuff _buff;

    private void OnDestroy()
    {
        _bag.AmountChanged -= OnAmountChanged;
    }

    public void Initialize(IBuff buff)
    {
        if (buff == null)
            throw new ArgumentNullException(nameof(buff));

        _buff = buff;
    }

    [Inject]
    private void Inject(BuffBag bag)
    {
        _bag = bag;
        _bag.AmountChanged += OnAmountChanged;

        OnAmountChanged();
    }

    private void OnAmountChanged()
    {
        int count = _bag.GetCount(_buff);

        _text.text = count.ToString();
    }
}
