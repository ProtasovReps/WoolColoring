using UnityEngine;
using System.Collections.Generic;
using System;

public class FigureConveyer : IUnsubscribable
{
    private readonly PositionDatabase _positionDatabase;
    private readonly FigurePool _figurePool;
    private readonly int _minFiguresCount;

    public FigureConveyer(FigureFactory factory, IReadOnlyCollection<ConveyerPosition> positions, int minFiguresCount)
    {
        if (positions.Count == 0)
            throw new EmptyCollectionException();

        if (factory == null)
            throw new ArgumentNullException(nameof(factory));

        if (minFiguresCount <= 0 || minFiguresCount >= positions.Count)
            throw new ArgumentException(nameof(minFiguresCount));

        _positionDatabase = new PositionDatabase(positions);
        _figurePool = new FigurePool(factory);
        _minFiguresCount = minFiguresCount;

        Subscribe();
        FillAllFigures();
    }

    public void Unsubscribe()
    {
        _positionDatabase.PositionChanged -= OnPositionChanged;
    }

    private void Subscribe()
    {
        _positionDatabase.PositionChanged += OnPositionChanged;
    }

    private void AddFigure()
    {
        Figure newFigure = _figurePool.Get();

        newFigure.Falled += RemoveFigure;
        _positionDatabase.Add(newFigure);
    }

    private void RemoveFigure(Figure figure)
    {
        figure.Falled -= RemoveFigure;

        _positionDatabase.Remove(figure);
        _figurePool.Release(figure);

        if (_positionDatabase.TransformablesCount < _minFiguresCount)
            FillAllFigures();
    }

    private void FillAllFigures()
    {
        while (_positionDatabase.PositionsCount > _positionDatabase.TransformablesCount)
            AddFigure();
    }

    private void OnPositionChanged(ITransformable transformable, Vector3 position)
    {
        transformable.SetPosition(position);
    }
}
