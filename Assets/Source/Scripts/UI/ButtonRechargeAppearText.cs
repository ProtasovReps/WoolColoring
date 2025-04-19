using UnityEngine;

public class ButtonRechargeAppearText : TemporaryActivatableUI
{
    [SerializeField] private RechargeableButton[] _rechargeableButton;

    private void OnDestroy()
    {
        foreach (var button in _rechargeableButton)
            button.Recharged -= OnRecharged;
    }

    public override void Initialize()
    {
        foreach (var button in _rechargeableButton)
            button.Recharged += OnRecharged;

        base.Initialize();
    }

    private void OnRecharged() => Activate();
}
