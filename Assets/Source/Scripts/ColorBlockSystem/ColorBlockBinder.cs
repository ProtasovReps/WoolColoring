using System.Collections.Generic;
using ColorBlockSystem.Presenter;
using ColorBlockSystem.View;
using ColorBlockSystem.Model;
using RopeSystem;
using Extensions.View;

namespace ColorBlockSystem
{
    public class ColorBlockBinder
    {
        private readonly ColorBlockView[] _colorBlockViews;
        private readonly BlockHolderConnector _blockHolderConnector;
        private readonly ObjectDisposer _disposer;

        public ColorBlockBinder(ColorBlockView[] colorBlockViews, BlockHolderConnector blockHolderConnector, ObjectDisposer disposer)
        {
            _colorBlockViews = colorBlockViews;
            _blockHolderConnector = blockHolderConnector;
            _disposer = disposer;
        }

        public ColorBlock[] Bind()
        {
            var colorBlockModels = new List<ColorBlock>();

            foreach (ColorBlockView view in _colorBlockViews)
            {
                var model = new ColorBlock(view.RequiredColor);
                var presenter = new ColorBlockPresenter(view, model, _blockHolderConnector);

                _disposer.Add(presenter);
                colorBlockModels.Add(model);
            }

            return colorBlockModels.ToArray();
        }
    }
}