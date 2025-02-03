using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ColoredStringHolderStash
{
    private readonly ColoredStringHolder[] _activeStringHolders;
    private readonly ColoredStringHolder[] _lockedStringHolders;

    public IReadOnlyCollection<IFillable<StringHolder>> ColoredStringHolders => _activeStringHolders;

    public ColoredStringHolderStash(ColoredStringHolder[] stringHolders, int activeCount)
    {
        if (stringHolders == null)
            throw new NullReferenceException(nameof(stringHolders));

        if (stringHolders.Length < activeCount)
            throw new ArgumentOutOfRangeException(nameof(activeCount));

        if (activeCount <= 0)
            throw new ArgumentOutOfRangeException(nameof(activeCount));

        _activeStringHolders = new ColoredStringHolder[activeCount];
        _lockedStringHolders = new ColoredStringHolder[stringHolders.Length - activeCount];

        InitializeArrays(stringHolders, activeCount);
    }

    public bool TryGetColoredStringHolder(Color requiredColor, out ColoredStringHolder holder)
    {
        if (requiredColor == null)
            throw new ArgumentNullException(nameof(requiredColor));

        holder = _activeStringHolders.FirstOrDefault(holder => holder.RequiredColor == requiredColor);
        return holder != null;
    }

    private void InitializeArrays(ColoredStringHolder[] stringHolders, int activeCount)
    {
        int unlockedIndex = 0;
        int lockedIndex = 0;

        for (int i = 0; i < stringHolders.Length; i++)
        {
            if (i < activeCount)
            {
                _activeStringHolders[unlockedIndex] = stringHolders[i];
                unlockedIndex++;
            }
            else
            {
                _lockedStringHolders[lockedIndex] = stringHolders[i];
                lockedIndex++;
            }
        }
    }
}