using System.Collections.Generic;
using UnityEngine;

public class StringHolderView : MonoBehaviour
{
    [SerializeField] private ColorString[] _strings;

    private StringHolderPresenter _presenter;

    public IEnumerable<ColorString> Strings => _strings;

    public virtual void Initialize(StringHolderPresenter presenter)
    {
        _presenter = presenter;
    }

    public void AddString(ColorString colorString)
    {
        colorString.gameObject.SetActive(true);
    }

    public void RemoveString(ColorString colorString)
    {
        colorString.gameObject.SetActive(false);
    }
}
