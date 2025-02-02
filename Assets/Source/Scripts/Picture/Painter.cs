using System;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private Picture _picture;

    private ColoredStringHolderStash _holderStash;
    private ColoredStringHolderSwitcher _switcher;

    public void Initialize(ColoredStringHolderSwitcher switcher, ColoredStringHolderStash stash)
    {
        if (_picture == null)
            throw new NullReferenceException(nameof(_holderStash));

        if (switcher == null)
            throw new NullReferenceException(nameof(switcher));

        if(stash == null)
            throw new NullReferenceException(nameof(stash));

        _holderStash = stash;
        _switcher = switcher;
    }

    private void OnEnable()
    {
        foreach (IFillable<StringHolder> holder in _holderStash.ColoredStringHolders)
            holder.Filled += OnFilled;
    }

    private void OnDisable()
    {
        foreach (IFillable<StringHolder> holder in _holderStash.ColoredStringHolders)
            holder.Filled -= OnFilled;
    }

    private void OnFilled(StringHolder holder)
    {
        if (holder is ColoredStringHolder coloderHolder == false)
            throw new InvalidCastException();

        FillImage(coloderHolder);
    }

    private void FillImage(ColoredStringHolder holder)
    {
        for (int i = 0; i < holder.StringCount; i++)
        {
            Color color = holder.GetColorable().Color;

            _picture.Colorize(color);
        }

        Color requiredColor = _picture.GetRequiredColor();

        _switcher.Switch(requiredColor, holder);
    }
}
