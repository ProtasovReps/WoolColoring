using UnityEngine;

public class ColoredStringHolderSwitcher : MonoBehaviour
{
    public void Switch(Color requiredColor,  ColoredStringHolder coloredHolder)
    {
        coloredHolder.SetRequiredColor(requiredColor);
    }
}
