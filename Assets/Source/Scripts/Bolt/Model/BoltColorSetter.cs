using System;
using UnityEngine;

public class BoltColorSetter : IDisposable
{
    private readonly BoltStash _boltStash;
    private readonly Picture _picture;

    public BoltColorSetter(BoltStash boltStash, Picture picture)
    {
        if(boltStash == null)
            throw new NullReferenceException(nameof(boltStash));

        if(picture == null)
            throw new ArgumentNullException(nameof(picture));

        _boltStash = boltStash;
        _picture = picture;

        _picture.Filled += OnColorFilled;

        SetColors();
    }

    public void Dispose()
    {
        _picture.Filled -= OnColorFilled;
    }

    private void SetColors()
    {
        foreach (BoltView bolt in _boltStash.Bolts)
        {
            Color color = _picture.GetRandomColor();

            bolt.ColorSettable.SetColor(color);
        }
    }

    private void OnColorFilled(Color color)
    {
        foreach (BoltView bolt in _boltStash.Bolts)
        {
            if (bolt.Colorable.Color == color)
            {
                Color newColor = _picture.GetRandomColor();

                bolt.ColorSettable.SetColor(newColor);
            }
        }
    }
}
