using ColorBlockSystem.Model;
using ColorBlockSystem.View;
using RopeSystem;
using System;
using UnityEngine;

namespace ColorBlockSystem.Presenter
{
    public class ColorBlockPresenter : IDisposable
    {
        private readonly ColorBlockView _view;
        private readonly ColorBlock _model;
        private readonly BlockHolderConnector _connector;

        public ColorBlockPresenter(ColorBlockView view, ColorBlock model, BlockHolderConnector connector)
        {
            if (view == null)
                throw new NullReferenceException(nameof(view));

            if (model == null)
                throw new NullReferenceException(nameof(model));

            if (connector == null)
                throw new NullReferenceException(nameof(connector));

            _view = view;
            _model = model;
            _connector = connector;
            _model.ColorSetted += OnColorSetted;
        }

        public void Dispose()
        {
            _model.ColorSetted -= OnColorSetted;
        }

        private void OnColorSetted(Color color)
        {
            _view.SetColor(color);
            _connector.Setup(_view);
        }
    }
}