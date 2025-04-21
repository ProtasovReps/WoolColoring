using UnityEngine;

public class ActivateUiReplic : DefaultReplic
{
    [SerializeField] private LevelUI _levelUI;

    public override void Activate()
    {
        _levelUI.gameObject.SetActive(true);
        base.Activate();
    }
}