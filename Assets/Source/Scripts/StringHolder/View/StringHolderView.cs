using System.Collections.Generic;
using UnityEngine;

public class StringHolderView : MonoBehaviour
{
    [SerializeField] private ColorStringView[] _strings;

    public IEnumerable<ColorStringView> Strings => _strings;

    public virtual void Initialize()
    {
        for (int i = 0; i < _strings.Length; i++)
            _strings[i].transform.localScale = Vector3.zero;
    }
}