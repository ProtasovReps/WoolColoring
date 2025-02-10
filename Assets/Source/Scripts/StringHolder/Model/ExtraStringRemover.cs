using System;
using UnityEngine;

public class ExtraStringRemover : IUnsubscribable
{
    private readonly Picture _picture;
    private readonly WhiteStringHolder _whiteStringHolder;

    public ExtraStringRemover(Picture picture, WhiteStringHolder holder)
    {
        if (picture == null)
            throw new ArgumentNullException(nameof(picture));

        if (holder == null)
            throw new ArgumentNullException(nameof(holder));

        _picture = picture;
        _whiteStringHolder = holder;

        Subscribe();
    }

    public void Unsubscribe()
    {
        _picture.ColorFilled -= OnColorFilled;
    }

    private void Subscribe()
    {
        _picture.ColorFilled += OnColorFilled;
    }

    private void OnColorFilled(Color color)
    {
        int requiredStringsCount = _whiteStringHolder.GetRequiredColorsCount(color);

        if (requiredStringsCount == 0)
            return;

        for (int i = 0; i < requiredStringsCount; i++)
            _whiteStringHolder.GetRequiredColorable(color);
    }
}