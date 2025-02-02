using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ColoredStringHolderStash
{
    private ColoredStringHolder[] _activeStringHolders;
    private ColoredStringHolder[] _lockedStringHolders;

    public IReadOnlyCollection<IFillable<StringHolder>> ColoredStringHolders => _activeStringHolders;

    public ColoredStringHolderStash(ColoredStringHolder[] activeStringHolders, ColoredStringHolder[] lockedStringHolders)
    {
        if(activeStringHolders == null)
            throw new NullReferenceException(nameof(activeStringHolders));

        if(lockedStringHolders == null)
            throw new NullReferenceException(nameof(lockedStringHolders));

        _activeStringHolders = activeStringHolders;
        _lockedStringHolders = lockedStringHolders;
    }

    public bool TryGetColoredStringHolder(Color requiredColor, out ColoredStringHolder holder)
    {
        if(requiredColor == null)
            throw new ArgumentNullException(nameof(requiredColor));

        holder = _activeStringHolders.FirstOrDefault(holder => holder.RequiredColor == requiredColor);
        return holder != null;
    }
}