using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using System;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private float _colorizeDelay;
    [SerializeField, Min(1)] private int _blocksPerString;

    private Picture _picture;
    private ColoredStringHolderStash _holderStash;
    private ColoredStringHolderSwitcher _switcher;

    [Inject]
    private void Inject(Picture picture, ColoredStringHolderSwitcher switcher, ColoredStringHolderStash stash)
    {
        _picture = picture;
        _holderStash = stash;
        _switcher = switcher;

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

        FillImage(coloderHolder).Forget();
    }

    private async UniTaskVoid FillImage(ColoredStringHolder holder)
    {
        Color color = holder.Color;

        holder.SetEnabled(false);

        for (int i = 0; i < holder.MaxStringCount; i++)
        {
            holder.GetLastString();

            for (int j = 0; j < _blocksPerString; j++)
            {
                _picture.Colorize(color);
                await UniTask.WaitForSeconds(_colorizeDelay);
            }
        }

        holder.SetEnabled(true);
        _switcher.ChangeStringHolderColor(holder);
    }
}