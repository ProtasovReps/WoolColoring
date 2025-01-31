using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StringHolder : MonoBehaviour
{
    [SerializeField] private ColorString[] _strings;
    [SerializeField, Min(1)] private int _maxCapacity;

    public int StringCount => _strings.Length;
    public int MaxCapacity => _maxCapacity;
    protected IReadOnlyCollection<ColorString> Strings => _strings;

    private void Awake()
    {
        _maxCapacity = _strings.Length;
    }

    public void Add(ColorString newString)
    {
        ColorString freeString = GetFreeString();

        PrepareString(freeString, newString);

        freeString.gameObject.SetActive(true);
    }

    protected abstract void PrepareString(ColorString freeString, ColorString newString);

    private ColorString GetFreeString()
    {
        ColorString freeString = _strings.FirstOrDefault(colorString => colorString.gameObject.activeSelf);

        if (freeString == null)
            throw new ArgumentNullException(nameof(freeString));

        return freeString;
    }
}