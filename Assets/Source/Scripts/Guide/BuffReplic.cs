using UnityEngine;
using UnityEngine.UI;

public class BuffReplic : Replic
{
    [SerializeField] private Button _buff;
    [SerializeField] private BuffButton _buffButton;

    public override void Activate()
    {
        _buff.interactable = true;
        _buffButton.SetIsNotCooldowning(true);
        _buff.onClick.AddListener(OnBuffClicked);
        base.Activate();
    }

    protected override void Deactivate()
    {
        _buff.interactable = false;
        _buff.onClick.RemoveListener(OnBuffClicked);
        _buffButton.SetIsNotCooldowning(false);
        base.Deactivate();
    }

    protected override void OnAnimationFinalized() { }

    private void OnBuffClicked() => Deactivate();
}