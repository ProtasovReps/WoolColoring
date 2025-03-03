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

    public void Shake()
    {
        Vector3 targetScale = Transform.localScale * 0.8f;

        LSequence.Create()
            .Append(LMotion.Create(Transform.localScale, targetScale, 0.4f).WithEase(Ease.InElastic).BindToLocalScale(Transform))
            .Append(LMotion.Create(targetScale, TransformView.StartScale, 0.4f).WithEase(Ease.OutElastic).BindToLocalScale(Transform))
            .Run();
    }

    private void Appear()
    {
        LMotion.Create(Vector3.zero, TransformView.StartScale, _appearDuration)
            .WithEase(Ease.InOutElastic)
            .BindToLocalScale(Transform);
    }
}
