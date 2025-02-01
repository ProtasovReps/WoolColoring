using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class ColoredStringHolderStash : MonoBehaviour
{
    [SerializeField] private ColoredStringHolder[] _stringHolders;

    public IReadOnlyCollection<IFillable<StringHolder>> ColoredStringHolders => _stringHolders;

    private void Awake()
    {
        if(_stringHolders.Length == 0)
            throw new ArgumentException($"{nameof(_stringHolders)} is empty");
    }

    public bool TryGetColoredStringHolder(Color requiredColor, out ColoredStringHolder holder)
    {
        if(requiredColor == null)
            throw new ArgumentNullException(nameof(requiredColor));

        holder = _stringHolders.FirstOrDefault(holder => holder.RequiredColor == requiredColor);
        return holder != null;
    }
}