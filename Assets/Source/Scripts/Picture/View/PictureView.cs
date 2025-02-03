using System;
using System.Collections.Generic;
using UnityEngine;

public class PictureView : MonoBehaviour
{
    [SerializeField] private ColorBlockView[] _colorBlocks;

    public IReadOnlyCollection<ColorBlockView> ColorBlocks => _colorBlocks;
}
