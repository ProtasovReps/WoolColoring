using System;
using System.Linq;
using UnityEngine;

public class ColoredStringHolder : StringHolder
{
    // пока что так, потом сделаем отдельный скрипт под цвета
    public Color RequiredColor { get; private set; }

    private void Awake()
    {
        RequiredColor = Strings.First().Color;
    }

    protected override void PrepareString(IColorable freeString, IColorable newString)
    {
        if (newString.Color != freeString.Color)
            throw new ArgumentException(nameof(newString));
    }
}
