using StringHolderSystem.Model;
using StringHolderSystem.View;
using System;
using UnityEngine;

namespace StringHolderSystem.Presenter
{
    public class ColoredStringHolderPresenter : IDisposable
    {
        private readonly ColoredStringHolder _model;
        private readonly ColoredStringHolderView _view;

        public ColoredStringHolderPresenter(ColoredStringHolderView view, ColoredStringHolder model)
        {
            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (model == null)
                throw new ArgumentNullException(nameof(model));

            _view = view;
            _model = model;

            _model.StringAdded += OnStringAdded;
            _model.ColorChanged += OnColorChanged;
            _model.Filled += holder => OnFilled();
        }

        public void Dispose()
        {
            _model.StringAdded -= OnStringAdded;
            _model.ColorChanged -= OnColorChanged;
        }

        public Color GetColor()
        {
            return _model.Color;
        }

        private void OnColorChanged()
        {
            _view.Switch();
        }

        private void OnStringAdded()
        {
            _view.Shake();
        }

        private void OnFilled()
        {
            _view.PlayFilledSound();
        }
    }
}