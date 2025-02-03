using System;
using System.Collections.Generic;
using UnityEngine;

public class PictureView : MonoBehaviour
{
    [SerializeField] private ColorBlockView[] _colorBlocks;

    private PicturePresenter _picturePresenter;

    public IReadOnlyCollection<ColorBlockView> ColorBlocks => _colorBlocks;

    public void Initialize(PicturePresenter presenter, List<ColorBlock> colorBlocks)
    {
        if (presenter == null)
            throw new NullReferenceException(nameof(presenter));

        presenter.Initialize(colorBlocks);
    }
}
