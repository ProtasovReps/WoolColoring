using System.Collections.Generic;

public class ColorBlockBinder
{
    public List<ColorBlock> Bind(IEnumerable<ColorBlockView> views, BlockHolderConnector connector)
    {
        var colorBlockModels = new List<ColorBlock>();

        foreach (ColorBlockView view in views)
        {
            var model = new ColorBlock(view.RequiredColor);
            var presenter = new ColorBlockPresenter(view, model, connector);

            colorBlockModels.Add(model);
        }

        return colorBlockModels;
    }
}
