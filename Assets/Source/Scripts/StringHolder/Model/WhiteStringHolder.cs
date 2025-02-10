using UnityEngine;

public class WhiteStringHolder : StringHolder
{
    private Color _requiredColor;

    public WhiteStringHolder(ColorString[] strings) : base(strings) { }

    public IColorable GetRequiredColorable(Color color)
    {
        _requiredColor = color;
        return GetColorable();
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
}
