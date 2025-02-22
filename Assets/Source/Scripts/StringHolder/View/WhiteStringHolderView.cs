using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

[RequireComponent(typeof(TransformView))]
public class WhiteStringHolderView : StringHolderView
{
    private TransformView _transform;

    public void Initialize()
    {
        _transform = GetComponent<TransformView>();

        _transform.Initialize();
        Appear();
    }

    private void Appear()
    {
        LMotion.Create(Vector3.zero, _transform.StartScale, 1f)
            .WithEase(Ease.InOutElastic)
            .BindToLocalScale(_transform.Transform);
    }
}
