using LitMotion;
using LitMotion.Extensions;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TransformView))]
public class StringHolderView : MonoBehaviour
{
    [SerializeField] private ColorStringView[] _strings;
    [SerializeField] private float _shakeDuration;

    private TransformView _transformView;

    public IEnumerable<ColorStringView> Strings => _strings;
    public Transform Transform => _transformView.Transform;
    protected TransformView TransformView => _transformView;

    public virtual void Initialize()
    {
        _transformView = GetComponent<TransformView>();
        _transformView.Initialize();
    }

    public void Shake()
    {
        Vector3 targetScale = Transform.localScale * 0.95f;

        LSequence.Create()
            .Append(LMotion.Create(Transform.localScale, targetScale, _shakeDuration).WithEase(Ease.InElastic).BindToLocalScale(Transform))
            .Append(LMotion.Create(targetScale, TransformView.StartScale, _shakeDuration).WithEase(Ease.OutElastic).BindToLocalScale(Transform))
            .Run();
    }

    public bool TryGetFreeStringTransform(out Transform transform)
    {
        for (int i = 0; i < _strings.Length; i++)
        {
            if (_strings[i].gameObject.activeSelf == false)
            {
                transform = _strings[i].Transform;
                return true;
            }

        }

        transform = null;
        return false;
    }
}