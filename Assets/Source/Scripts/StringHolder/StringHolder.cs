using System;
using System.Linq;
using UnityEngine;

public abstract class StringHolder : MonoBehaviour
{
    [SerializeField] private ColorString[] _strings;
    [SerializeField] private int _maxCapacity;

    public int StringCount => _strings.Length;
    public int MaxCapacity => _maxCapacity;

    private void Awake()
    {
        _maxCapacity = _strings.Length;
    }

    public void Add(ColorString newString)
    {
        PrepareString(newString);

        ColorString freeString = _strings.FirstOrDefault(colorString => colorString.gameObject.activeSelf);

        if (freeString == null)
            throw new ArgumentNullException(nameof(freeString));

        freeString.gameObject.SetActive(true);
    }

    protected abstract void PrepareString(ColorString newString);
}