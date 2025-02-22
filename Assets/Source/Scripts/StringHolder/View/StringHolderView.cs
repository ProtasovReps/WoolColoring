using System.Collections.Generic;
using UnityEngine;

public class StringHolderView : MonoBehaviour
{
    [SerializeField] private ColorStringView[] _strings;

    public IEnumerable<ColorStringView> Strings => _strings;
}