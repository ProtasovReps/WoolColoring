using UnityEngine;
using UnityEngine.UI;

namespace PlayerGuide
{
    public class MenuActivateReplic : Replic
    {
        [SerializeField] private Button[] _buttonsToActivate;

        protected override void OnAnimationFinalized() { }
    }
}