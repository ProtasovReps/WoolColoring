using UnityEngine;

public class DeactivateButton : ButtonView
{
    [SerializeField] private Activatable _activatable;

    protected override void OnButtonClick()
    {
        _activatable.Deactivate();
    }
}
