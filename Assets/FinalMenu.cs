using Reflex.Attributes;
using UnityEngine;

[RequireComponent (typeof(ActivatableUI))]
public class FinalMenu : MonoBehaviour
{
    private ActivatableUI _activatable;
    private Picture _picture;

    [Inject]
    private void Inject(Picture picture)
    {
        _picture = picture;
        _activatable = GetComponent<ActivatableUI>();
        _picture.Colorized += Activate;
    }

    private void OnDestroy()
    {
        _picture.Colorized -= Activate;
    }

    private void Activate()
    {
        OnDestroy();

        _activatable.Activate();
    }
}