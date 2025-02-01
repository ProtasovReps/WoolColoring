using System;
using UnityEngine;

public class StringDistributor : MonoBehaviour
{
    [SerializeField] private WhiteStringHolder _whiteHolder;
    [SerializeField] private ColoredStringHolderStash _coloredHolderStash;

    public void Distribute(StringBolt bolt)
    {
        if (bolt == null)
            throw new ArgumentNullException(nameof(bolt));

        IColorable colorString = bolt.ColorString;

        if (_coloredHolderStash.TryGetColoredStringHolder(colorString.Color, out ColoredStringHolder holder))
            holder.Add(colorString);
        else
            _whiteHolder.Add(colorString);
    }
}
