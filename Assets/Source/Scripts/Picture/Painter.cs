using System;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private Picture _picture;
    [SerializeField] private ColoredStringHolderStash _holderStash;
    [SerializeField] private ColoredStringHolderSwitcher _switcher;

    private void Awake()
    {
        if (_holderStash == null)
            throw new NullReferenceException(nameof(_holderStash));
    }

    private void OnEnable()
    {
        foreach (IFillable<ColoredStringHolder> holder in _holderStash.ColoredStringHolders)
            holder.Filled += OnFilled;
    }

    private void OnDisable()
    {
        foreach (IFillable<ColoredStringHolder> holder in _holderStash.ColoredStringHolders)
            holder.Filled -= OnFilled;
    }

    private void OnFilled(ColoredStringHolder holder) => FillImage(holder);

    private void FillImage(ColoredStringHolder holder)
    {
        foreach (IColorable colorString in holder.Strings)
            _picture.Colorize(colorString.Color);

        Color requiredColor = _picture.GetRequiredColor();
        _switcher.Switch(requiredColor, holder);
    }
}
