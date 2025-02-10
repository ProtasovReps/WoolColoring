using System;
using UnityEngine;

public class StringHolderColorPresenter : IUnsubscribable
{
    private readonly ColoredStringHolderStash _stash;
    private readonly Picture _picture;

    public StringHolderColorPresenter(ColoredStringHolderStash stash, Picture picture)
    {
        if(stash == null)
            throw new NullReferenceException(nameof(stash));

        if(picture == null)
            throw new NullReferenceException(nameof(picture));

        _stash = stash;
        _picture = picture;

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
        foreach (ColoredStringHolder holder in _stash.ColoredStringHolders)
        {
            if(holder.Color == color)
            {
                Color newColor = _picture.GetRandomColor();

                holder.SetColor(newColor);
            }
        }
    }
}
