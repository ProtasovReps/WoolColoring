using System.Collections.Generic;
using UnityEngine;

public class ColorBlockViewStash : MonoBehaviour
{
    [SerializeField] private ColorBlockView[] _colorBlockViews;

    public IEnumerable<ColorBlockView> ColorBlockViews => _colorBlockViews;
}
