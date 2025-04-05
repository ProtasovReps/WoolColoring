using Reflex.Attributes;
using System;
using TMPro;
using UnityEngine;

public class BuffCountCounter : MonoBehaviour
{
    private const string ZeroBuffsSign = "+";

    [SerializeField] private TMP_Text _text;

    private BuffBag _bag;
    private IBuff _buff;

    private void OnDestroy()
    {
        _bag.AmountChanged -= buff => OnAmountChanged();
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
        _bag.AmountChanged += buff => OnAmountChanged();

        OnAmountChanged();
    }

    private void OnAmountChanged()
    {
        int count = _bag.GetCount(_buff);

        if (count == 0)
            _text.text = ZeroBuffsSign;
        else
            _text.text = count.ToString();
    }
}
