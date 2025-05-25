using System;
using UnityEngine;
using CustomInterface;

namespace FigurePlatform.Model
{
    public class Conveyer : IDisposable
    {
        private const int MinFiguresCount = 6;

        private readonly FigureCompositionPool _figurePool;
        private readonly PositionDatabase _positionDatabase;

        public Conveyer(FigureCompositionPool pool, PositionDatabase positionDatabase)
        {
            _figurePool = pool;
            _positionDatabase = positionDatabase;
            _positionDatabase.PositionChanged += OnPositionChanged;
        }

        public void Dispose()
        {
            _positionDatabase.PositionChanged -= OnPositionChanged;
        }

        public void FillAllFigures()
        {
            while (_positionDatabase.PositionsCount > _positionDatabase.TransformablesCount)
                AddFigure();
        }

        private void AddFigure()
        {
            FigureComposition newComposition = _figurePool.Get();

            newComposition.Emptied += RemoveFigure;
            _positionDatabase.Add(newComposition);
        }

        private void RemoveFigure(FigureComposition composition)
        {
            composition.Emptied -= RemoveFigure;

            _positionDatabase.Remove(composition);
            _figurePool.Release(composition);

            if (_positionDatabase.TransformablesCount <= MinFiguresCount)
                FillAllFigures();
        }

        private void OnPositionChanged(IPositionSettable transformable, Vector3 position)
        {
            transformable.SetPosition(position);
        }
    }
}