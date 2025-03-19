using UnityEngine;

public class ActivateButton : ButtonView
{
    [SerializeField] private Activatable _activatable;

    protected override void OnButtonClick()
    {
        base.OnButtonClick();
        _activatable.Activate();
    }
}