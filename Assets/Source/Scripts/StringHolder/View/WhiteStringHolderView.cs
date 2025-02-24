using LitMotion;
using LitMotion.Extensions;
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
        LMotion.Create(Vector3.zero, TransformView.StartScale, _appearDuration)
            .WithEase(Ease.InOutElastic)
            .BindToLocalScale(Transform);
    }
}
