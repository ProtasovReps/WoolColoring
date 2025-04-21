using UnityEngine;
using UnityEngine.UI;

public class MenuActivateReplic : Replic
{
    [SerializeField] private Button[] _buttonsToActivate;

    protected override void OnAnimationFinalized() { }
}