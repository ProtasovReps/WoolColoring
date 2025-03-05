using UnityEngine;
using UnityEngine.UI;

public class ActivatableButton : Activatable
{
    [SerializeField] private Button _button;

    public override void Activate()
    {
       _button.interactable = true;
    }

    public override void Deactivate()
    {
        _button.interactable = false;
    }
}
