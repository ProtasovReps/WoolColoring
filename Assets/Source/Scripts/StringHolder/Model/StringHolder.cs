using System;
using System.Collections.Generic;

public abstract class StringHolder : IFillable<StringHolder>
{
    private ColorString[] _strings;
    private int _stringCount;

    public event Action<StringHolder> Filled;

    public StringHolder(ColorString[] strings)
    {
        if (strings == null)
            throw new ArgumentNullException(nameof(strings));

        _strings = strings;
    }

    public int MaxStringCount => _strings.Length;
    protected int StringCount => _stringCount;
    protected IEnumerable<ColorString> Strings => _strings;

    public void Add(IColorable newString)
    {
        ColorString colorString = GetInactiveString();

        PrepareString(colorString, newString);

        colorString.SetEnable(true);
        _stringCount++;

        if (_stringCount == MaxStringCount)
            Filled?.Invoke(this);
    }

    protected IColorable GetString()
    {
        ColorString colorString = GetLastString();

        colorString.SetEnable(false);
        _stringCount--;

        return colorString;
    }

    protected abstract void PrepareString(IColorSettable freeString, IColorable newString);

    protected abstract bool IsValidString(IColorable colorString);

    protected bool IsActiveString(ColorString colorString, bool isActive)
       => colorString.IsEnabled == isActive;

    private ColorString GetLastString()
    {
        for (int i = _strings.Length - 1; i >= 0; i--)
        {
            if (IsActiveString(_strings[i], true) && IsValidString(_strings[i]))
            {
                return _strings[i];
            }
        }

        throw new InvalidOperationException();
    }

    private ColorString GetInactiveString()
    {
        for (int i = 0; i < _strings.Length; i++)
        {
            if (IsActiveString(_strings[i], false))
            {
                return _strings[i];
            }
        }

        throw new InvalidOperationException();
    }
}