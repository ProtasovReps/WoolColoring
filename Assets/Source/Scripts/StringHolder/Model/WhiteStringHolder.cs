using UnityEngine;

public class WhiteStringHolder : StringHolder
{
    private Color _requiredColor;

    public WhiteStringHolder(ColorString[] strings) : base(strings) { }

    public void SetRequiredColor(Color color) => _requiredColor = color;

    public int GetRequiredColorsCount()
    {
        int requiredColorsCount = 0;

        foreach (ColorString colorString in Strings)
        {
            if (IsActiveString(colorString, false))
                continue;

            if (colorString.Color != _requiredColor)
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
