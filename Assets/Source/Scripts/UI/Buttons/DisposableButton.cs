public class DisposableButton : ActivateButton
{
    protected override void OnButtonClick() // ����� ����������� �����-���� ��������
    {
        base.OnButtonClick();
        Deactivate();
    }
}