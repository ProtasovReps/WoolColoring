using System;
using StringHolderSystem.Model;
using StringHolderSystem.View;

namespace StringHolderSystem.Presenter
{
    public class WhiteStringHolderPresenter : IDisposable
    {
        private readonly WhiteStringHolder _model;
        private readonly WhiteStringHolderView _view;

        public WhiteStringHolderPresenter(WhiteStringHolder model, WhiteStringHolderView view)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            if (view == null)
                throw new ArgumentNullException(nameof(view));

            _model = model;
            _view = view;

            _model.StringAdded += OnStringAdded;
        }

        public void Dispose()
        {
            _model.StringAdded -= OnStringAdded;
        }

        private void OnStringAdded()
        {
            _view.Shake();
        }
    }
}