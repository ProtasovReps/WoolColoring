using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TransformView))]
public class StringHolderView : MonoBehaviour
{
    [SerializeField] private ColorStringView[] _strings;

    private TransformView _transformView;

    public IEnumerable<ColorStringView> Strings => _strings;
    public Transform Transform => _transformView.Transform;
    protected TransformView TransformView => _transformView;

    public virtual void Initialize()
    {
        _transformView = GetComponent<TransformView>();
        _transformView.Initialize();
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