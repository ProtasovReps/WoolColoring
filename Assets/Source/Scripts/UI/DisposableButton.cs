public class DisposableButton : ActivateButton
{
    protected override void OnButtonClick() // нужно прокидывать какое-либо действие
    {
        base.OnButtonClick();
        Deactivate();
    }
}