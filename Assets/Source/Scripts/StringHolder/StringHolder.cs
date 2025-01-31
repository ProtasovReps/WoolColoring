using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StringHolder : MonoBehaviour
{
    [SerializeField] private ColorString[] _strings;

    public int StringCount => _strings.Where(colorString => colorString.gameObject.activeSelf).Count();
    public int MaxCapacity => _strings.Length;
    protected IReadOnlyCollection<IColorable> Strings => _strings;

    public void Add(IColorable newString)
    {
        ColorString freeString = GetFreeString();

        PrepareString(freeString, newString);

        freeString.gameObject.SetActive(true);
    }

    protected abstract void PrepareString(IColorable freeString, IColorable newString);

    private ColorString GetFreeString()
    {
        ColorString freeString = _strings.FirstOrDefault(colorString => colorString.gameObject.activeSelf == false);

        if (freeString == null)
            throw new ArgumentNullException(nameof(freeString));

        return freeString;
    }
}