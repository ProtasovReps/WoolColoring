using System;
using System.Collections;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private float _colorizeDelay;
    [SerializeField, Min(1)] private int _blocksPerString;

    private Picture _picture;
    private ColoredStringHolderStash _holderStash;
    private ColoredStringHolderSwitcher _switcher;
    private WaitForSeconds _delay;

    public void Initialize(Picture picture, ColoredStringHolderSwitcher switcher, ColoredStringHolderStash stash)
    {
        if (picture == null)
            throw new NullReferenceException(nameof(_holderStash));

        if (switcher == null)
            throw new NullReferenceException(nameof(switcher));

        if (stash == null)
            throw new NullReferenceException(nameof(stash));

        _picture = picture;
        _holderStash = stash;
        _switcher = switcher;
        _delay = new WaitForSeconds(_colorizeDelay);

        foreach (IFillable<StringHolder> holder in _holderStash.ColoredStringHolders)
            holder.Filled += OnHolderFilled;
    }

    private void OnDestroy()
    {
        foreach (IFillable<StringHolder> holder in _holderStash.ColoredStringHolders)
            holder.Filled -= OnHolderFilled;
    }

    private void OnHolderFilled(StringHolder holder)
    {
        if (holder is ColoredStringHolder coloderHolder == false)
            throw new InvalidCastException();

        StartCoroutine(FillImage(coloderHolder));
    }

    private IEnumerator FillImage(ColoredStringHolder holder)
    {
        Color color = holder.Color;

        for (int i = 0; i < holder.MaxStringCount; i++)
        {
            holder.GetLastString();

            for (int j = 0; j < _blocksPerString; j++)
            {
                _picture.Colorize(color);
                yield return _delay;
            }
        }

        _switcher.ChangeStringHolderColor(holder);
    }
}