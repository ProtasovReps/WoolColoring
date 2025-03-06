using UnityEngine;

public class WhiteStringHolderView : StringHolderView
{
    [SerializeField] private float _appearDuration;

    public override void Initialize()
    {
        base.Initialize();
        Appear();
    }

    private void Appear()
    {
        Animations.Appear(Transform);
    }
}