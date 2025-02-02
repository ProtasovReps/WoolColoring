using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StringHolder : MonoBehaviour, IFillable<StringHolder>
{
    [SerializeField] private ColorString[] _strings;

    public event Action<StringHolder> Filled;

    public int MaxStringCount => _strings.Length;
    public int StringCount => _strings.Where(colorString => colorString.gameObject.activeSelf).Count();
    public IReadOnlyCollection<IColorable> Strings => _strings;

    public void Add(IColorable newString)
    {
        ColorString colorString = GetFreeString();

        PrepareString(colorString, newString);

        colorString.gameObject.SetActive(true);

        if (StringCount == MaxStringCount)
            Filled?.Invoke(this);
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