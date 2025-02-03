public class WhiteStringHolder : StringHolder
{
    protected override void PrepareString(IColorable freeString, IColorable newString)
    {
        freeString.SetColor(newString.Color);
    }
}
