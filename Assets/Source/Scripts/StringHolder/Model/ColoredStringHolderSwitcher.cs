using UnityEngine;

public class ColoredStringHolderSwitcher
{
    public void Switch(Color requiredColor,  ColoredStringHolder coloredHolder)
    {
        coloredHolder.SetRequiredColor(requiredColor);
    }
}
