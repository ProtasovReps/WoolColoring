public class WhiteStringHolder : StringHolder
{
    protected override void PrepareString(ColorString freeString, ColorString newString)
    {
        freeString.SetColor(newString.Color);
    }
}
