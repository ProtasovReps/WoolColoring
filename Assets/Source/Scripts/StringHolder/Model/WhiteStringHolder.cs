using System;
using UnityEngine;

public class WhiteStringHolder : StringHolder, IFillable<WhiteStringHolder>
{
    private Color _requiredColor;

    public event Action<WhiteStringHolder> Filled;

    public WhiteStringHolder(ColorString[] strings) : base(strings) { }

    public IColorable GetRequiredColorable(Color color)
    {
        _requiredColor = color;
        return GetString();
    }

    public int GetRequiredColorsCount(Color color)
    {
        int requiredColorsCount = 0;

        foreach (ColorString colorString in Strings)
        {
            if (IsActiveString(colorString, false))
                continue;

            if (colorString.Color != color)
                continue;

            requiredColorsCount++;
        }

        return requiredColorsCount;
    }

    protected override void PrepareString(IColorSettable freeString, IColorable newString)
        => freeString.SetColor(newString.Color);

    protected override bool IsValidString(IColorable colorString)
        => colorString.Color == _requiredColor;

    protected override void OnFilled() => Filled?.Invoke(this);
}