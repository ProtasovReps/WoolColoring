using System;
using UnityEngine;

public class BoltColorSetter : IDisposable
{
    private readonly BoltStash _boltStash;
    private readonly Picture _picture;

    public BoltColorSetter(BoltStash stash, Picture picture)
    {
        if (stash == null)
            throw new ArgumentNullException(nameof(stash));

        if (picture == null)
            throw new ArgumentNullException(nameof(picture));

        _boltStash = stash;
        _picture = picture;
        _picture.Filled += OnColorFilled;
    }

    public void Dispose()
    {
        _picture.Filled -= OnColorFilled;
    }

    public void SetColors()
    {
        foreach (Bolt bolt in _boltStash.Bolts)
        {
            Color color = _picture.GetRandomColor();

            bolt.ColorSettable.SetColor(color);
        }
    }

    private void OnColorFilled(Color color)
    {
        foreach (Bolt bolt in _boltStash.Bolts)
        {
            if (bolt.Colorable.Color == color)
            {
                Color newColor = _picture.GetRandomColor();

                bolt.ColorSettable.SetColor(newColor);
            }
        }
    }
}