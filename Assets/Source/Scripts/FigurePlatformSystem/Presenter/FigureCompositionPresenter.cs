using System;
using UnityEngine;
using FigurePlatform.Model;
using FigurePlatform.View;
using Extensions;

namespace FigurePlatform.Presenter
{
    public class FigureCompositionPresenter : IDisposable
    {
        private readonly FigureComposition _model;
        private readonly FigureCompositionView _view;
        private readonly ColorPallete _colorPallete;

        public FigureCompositionPresenter(FigureComposition model, FigureCompositionView view, ColorPallete colorPallete)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            if (colorPallete == null)
                throw new ArgumentNullException(nameof(colorPallete));

            _model = model;
            _view = view;
            _colorPallete = colorPallete;

            _model.Appeared += OnAppeared;
            _model.PositionChanged += OnPositionChanged;
            _model.Emptied += OnEmptied;
        }

        public void Dispose()
        {
            _model.Appeared -= OnAppeared;
            _model.PositionChanged -= OnPositionChanged;
            _model.Emptied -= OnEmptied;
        }

        private void OnAppeared()
        {
            Color newColor = _colorPallete.GetRandomColor();

            _model.SetColor(newColor);
            _view.Enable();
        }

        private void OnPositionChanged()
        {
            _view.Move(_model.Position);
        }

        private void OnEmptied(FigureComposition composition)
        {
            _view.Disable();
        }
    }
}
