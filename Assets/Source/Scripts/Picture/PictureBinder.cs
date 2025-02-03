using System.Collections.Generic;

public class PictureBinder
{
    public Picture Bind(PictureView view, ColorBlockBinder colorBlockBinder)
    {
        var blocks = new List<ColorBlock>(view.ColorBlocks.Count);
        var model = new Picture();
        var presenter = new PicturePresenter(model, view);

        foreach (ColorBlockView blockView in view.ColorBlocks)
            blocks.Add(colorBlockBinder.Bind(blockView));

        view.Initialize(presenter, blocks);

        return model;
    }
}
