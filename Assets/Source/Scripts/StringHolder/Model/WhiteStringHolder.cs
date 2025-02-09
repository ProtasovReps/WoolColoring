public class WhiteStringHolder : StringHolder
{
    public WhiteStringHolder(ColorString[] strings) : base(strings) { }

    protected override void PrepareString(IColorSettable freeString, IColorable newString)
    {
        freeString.SetColor(newString.Color);
    }
}
