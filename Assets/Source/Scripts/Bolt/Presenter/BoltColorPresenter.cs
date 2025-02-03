using System;
using UnityEngine;

public class BoltColorPresenter : IDisposable
{
    private readonly BoltStash _boltStash;
    private readonly Picture _picture;

    public BoltColorPresenter(BoltStash boltStash, Picture picture)
    {
        if(boltStash == null)
            throw new NullReferenceException(nameof(boltStash));

        if(picture == null)
            throw new ArgumentNullException(nameof(picture));

        _boltStash = boltStash;
        _picture = picture;

        Subscribe();
        SetColors();
    }

    public void Dispose() => Unsubscribe();

    private void Subscribe()
    {
        _picture.ColorFilled += OnColorFilled;
    }

    private void Unsubscribe()
    {
        _picture.ColorFilled -= OnColorFilled;
    }

    private void SetColors()
    {
        foreach (StringBolt bolt in _boltStash.Bolts)
        {
            Color color = _picture.GetRequiredColor();

            bolt.ColorString.SetColor(color);
        }
    }

    private void OnColorFilled(Color color)
    {
        foreach (StringBolt bolt in _boltStash.Bolts)
        {
            if (bolt.ColorString.Color == color)
            {
                Color newColor = _picture.GetRequiredColor();

                bolt.ColorString.SetColor(newColor);
            }
        }
    }
}
