using System;
using UnityEngine;
using FigurePlatformSystem.Model;
using FigurePlatformSystem.View;

namespace FigurePlatformSystem.Presenter
{
    public class FigurePresenter : IDisposable
    {
        private readonly Figure _model;
        private readonly FigureView _view;

        public FigurePresenter(Figure model, FigureView view)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            _model = model;
            _view = view;

            _model.Appeared += OnAppeared;
            _model.ColorChanged += OnColorChanged;
        }

        public void Dispose()
        {
            _model.Appeared -= OnAppeared;
            _model.ColorChanged -= OnColorChanged;
        }

        public void Fall()
        {
            _model.Fall();
        }

        private void OnAppeared()
        {
            _view.Appear();
        }

        private void OnColorChanged(Color color)
        {
            _view.SetColor(color);
        }
    }
}