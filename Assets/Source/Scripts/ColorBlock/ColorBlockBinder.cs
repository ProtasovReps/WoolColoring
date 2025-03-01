using System.Collections.Generic;

public class ColorBlockBinder
{
    private readonly ColorBlockView[] _colorBlockViews;
    private readonly BlockHolderConnector _blockHolderConnector;

    public ColorBlockBinder(ColorBlockView[] colorBlockViews, BlockHolderConnector blockHolderConnector)
    {
        _colorBlockViews = colorBlockViews;
        _blockHolderConnector = blockHolderConnector;
    }

    public ColorBlock[] Bind()
    {
        var colorBlockModels = new List<ColorBlock>();

        foreach (ColorBlockView view in _colorBlockViews)
        {
            var model = new ColorBlock(view.RequiredColor);
            var presenter = new ColorBlockPresenter(view, model, _blockHolderConnector);

            colorBlockModels.Add(model);
        }

        return colorBlockModels.ToArray();
    }
}
