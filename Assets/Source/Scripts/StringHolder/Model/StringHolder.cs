using System;
using System.Collections.Generic;
using System.Linq;

public abstract class StringHolder : IFillable<StringHolder>
{
    private IReadOnlyCollection<ColorString> _strings;

    public event Action<StringHolder> Filled;
    public event Action<ColorString> StringAdded;
    public event Action<ColorString> StringRemoved;

    public int MaxStringCount => _strings.Count;
    public int StringCount => _strings.Where(colorString => colorString.gameObject.activeSelf).Count();

    public void Initialize(IReadOnlyCollection<ColorString> strings)
    {
        if (strings == null)
            throw new ArgumentNullException(nameof(strings));

        _strings = strings;
    }

    public void Add(IColorable newString)
    {
        ColorString colorString = GetFreeString();

        PrepareString(colorString, newString);

        StringAdded?.Invoke(colorString);

        if (StringCount == MaxStringCount)
            Filled?.Invoke(this);
    }

    public IColorable GetColorable()
    {
        ColorString colorString = _strings.LastOrDefault(colorString => colorString.gameObject.activeSelf == true);

        if (colorString == null)
            throw new ArgumentNullException(nameof(colorString));

        StringRemoved?.Invoke(colorString);
        return colorString;
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