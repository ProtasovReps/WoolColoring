using System.Collections.Generic;
using UnityEngine;

public class PictureView : MonoBehaviour
{
    [SerializeField] private ColorBlockView[] _colorBlocks;

    public IEnumerable<ColorBlockView> ColorBlocks => _colorBlocks;
}
