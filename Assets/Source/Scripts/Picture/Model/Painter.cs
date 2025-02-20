using System;
using System.Collections;
using UnityEngine;

public class Painter : MonoBehaviour
{
    private Picture _picture;
    private ColoredStringHolderStash _holderStash;
    private ColoredStringHolderSwitcher _switcher;
    private WaitForSeconds _colorizeDelay;
    private int _blocksPerHolder;

    public void Initialize(Picture picture, ColoredStringHolderSwitcher switcher, ColoredStringHolderStash stash, int blocksPerHolder)
    {
        if (picture == null)
            throw new NullReferenceException(nameof(_holderStash));

        if (switcher == null)
            throw new NullReferenceException(nameof(switcher));

        if (stash == null)
            throw new NullReferenceException(nameof(stash));

        if (blocksPerHolder <= 0)
            throw new ArgumentException(nameof(blocksPerHolder));

        _picture = picture;
        _holderStash = stash;
        _switcher = switcher;
        _blocksPerHolder = blocksPerHolder;
        _colorizeDelay = new WaitForSeconds(0.1f);

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

        holder.GetAllStrings();

        for (int j = 0; j < _blocksPerHolder; j++)
        {
            _picture.Colorize(color);
            yield return _colorizeDelay;
        }

        _switcher.ChangeStringHolderColor(holder);
    }
}