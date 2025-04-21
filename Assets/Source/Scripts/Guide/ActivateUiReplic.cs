using UnityEngine;
using UnityEngine.UI;

public class ActivateUiReplic : DefaultReplic
{
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private Button[] _levelButtons;

    public override void Activate()
    {
        _levelUI.transform.gameObject.SetActive(true);

        for (int i = 0; i < _levelButtons.Length; i++)
            _levelButtons[i].gameObject.SetActive(true);

        base.Activate();
    }
}