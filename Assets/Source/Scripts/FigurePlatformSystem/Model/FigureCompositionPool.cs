using System;
using System.Collections.Generic;

namespace FigurePlatformSystem.Model
{
    public class FigureCompositionPool
    {
        private readonly FigureCompositionFactory _factory;
        private readonly Queue<FigureComposition> _disabledCompositions;

        public FigureCompositionPool(FigureCompositionFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
            _disabledCompositions = new Queue<FigureComposition>();
        }

        public FigureComposition Get()
        {
            FigureComposition composition;

            if (_disabledCompositions.Count == 0)
                _disabledCompositions.Enqueue(Create());

            composition = _disabledCompositions.Dequeue();
            composition.Appear();

            return composition;
        }

        public void Release(FigureComposition composition)
        {
            _disabledCompositions.Enqueue(composition);
        }

        private FigureComposition Create()
        {
            return _factory.Produce();
        }
    }
}
