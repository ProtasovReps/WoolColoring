using System.Collections.Generic;
using UnityEngine;

public class ColorBlockViewStash : MonoBehaviour
{
    private ColorBlockView[] _colorBlockViews;

    public IEnumerable<ColorBlockView> ColorBlockViews => _colorBlockViews;

    private void Awake()
    {
        ColorBlockView[] colorBlockViews = GetComponentsInChildren<ColorBlockView>();
        _colorBlockViews = new ColorBlockView[colorBlockViews.Length];

        for (int i = 0; i < _colorBlockViews.Length; i++)
            _colorBlockViews[i] = colorBlockViews[i];
    }
}
