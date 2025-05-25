using UnityEngine;

namespace LevelInterface.Buttons
{
    public class DeactivateButton : ButtonView
    {
        [SerializeField] private Activatable _activatable;

        protected override void OnButtonClick()
        {
            base.OnButtonClick();
            _activatable.Deactivate();
        }
    }
}
