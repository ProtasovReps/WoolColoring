using Reflex.Attributes;
using UnityEngine;

public class BuffTimerRechargeableButton : RechargeableButton
{
    [SerializeField] private float _rechargeTime = 30f;

    [Inject]
    private void Inject() => Initialize(new AdTimer(_rechargeTime));
}
