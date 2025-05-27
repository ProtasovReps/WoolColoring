using BoltSystem;
using FigurePlatformSystem.Model;
using FigurePlatformSystem.Presenter;
using Reflex.Attributes;
using UnityEngine;
using Extensions.View;

namespace FigurePlatformSystem.View
{
    public class FigureFactory : MonoBehaviour
    {
        private BoltStash _stash;
        private ObjectDisposer _disposer;

        [Inject]
        private void Inject(BoltStash stash)
        {
            _stash = stash;
        }

        public void Initialize(ObjectDisposer disposer)
        {
            _disposer = disposer;
        }

        public Figure Produce(FigureView view)
        {
            _stash.Add(view.Bolts);
            return BindFigure(view);
        }

        private Figure BindFigure(FigureView view)
        {
            var model = new Figure();
            var presenter = new FigurePresenter(model, view);

            _disposer.Add(presenter);
            view.Initialize(presenter);
            return model;
        }
    }
}