using System.Collections.Generic;

public class ColorBlockBinder
{
    public List<ColorBlock> Bind(IEnumerable<ColorBlockView> views)
    {
        var colorBlockModels = new List<ColorBlock>();

        foreach (ColorBlockView view in views)
        {
            var model = new ColorBlock(view.RequiredColor);
            var presenter = new ColorBlockPresenter(view, model);

            presenter.Subscribe();
            colorBlockModels.Add(model);
        }

        return colorBlockModels;
    }
}
