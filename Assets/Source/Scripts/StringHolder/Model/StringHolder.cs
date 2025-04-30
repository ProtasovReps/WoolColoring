using System;
using System.Collections.Generic;

public abstract class StringHolder
{
    private ColorString[] _strings;
    private int _stringCount;

    public event Action StringAdded;

    public StringHolder(ColorString[] strings)
    {
        if (strings == null)
            throw new ArgumentNullException(nameof(strings));

        _strings = strings;
    }

    public int MaxStringCount => _strings.Length;
    public int StringCount => _stringCount;
    protected IEnumerable<ColorString> Strings => _strings;

    public void Add(IColorable newString)
    {
        ColorString colorString = GetInactiveString();

        PrepareString(colorString, newString);

        colorString.SetEnable(true);
        _stringCount++;
        StringAdded?.Invoke();

        if (_stringCount >= MaxStringCount)
            OnFilled();
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

    protected abstract void OnFilled();

    protected bool IsEnabledString(ColorString colorString, bool isEnabled)
       => colorString.IsEnabled == isEnabled;

    private ColorString GetLastString()
    {
        for (int i = _strings.Length - 1; i >= 0; i--)
        {
            if (IsEnabledString(_strings[i], true) && IsValidString(_strings[i]))
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
            if (IsEnabledString(_strings[i], false))
            {
                return _strings[i];
            }
        }

        throw new InvalidOperationException();
    }
}