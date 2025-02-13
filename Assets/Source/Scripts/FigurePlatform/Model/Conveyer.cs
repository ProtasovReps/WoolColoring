using UnityEngine;
using System.Collections.Generic;
using System;

public class Conveyer : IUnsubscribable
{
    private readonly PositionDatabase _positionDatabase;
    private readonly FigureCompositionPool _figurePool;
    private readonly int _minFiguresCount;

    public Conveyer(FigureCompositionFactory factory, IReadOnlyCollection<ConveyerPosition> positions, int minFiguresCount)
    {
        if (positions.Count == 0)
            throw new EmptyCollectionException();

        if (factory == null)
            throw new ArgumentNullException(nameof(factory));

        if (minFiguresCount <= 0 || minFiguresCount >= positions.Count)
            throw new ArgumentException(nameof(minFiguresCount));

        _positionDatabase = new PositionDatabase(positions);
        _figurePool = new FigureCompositionPool(factory);
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
        FigureComposition newComposition = _figurePool.Get();

        newComposition.Emptied += RemoveFigure;
        _positionDatabase.Add(newComposition);
    }

    private void RemoveFigure(FigureComposition composition)
    {
        composition.Emptied -= RemoveFigure;

        _positionDatabase.Remove(composition);
        _figurePool.Release(composition);

        if (_positionDatabase.TransformablesCount <= _minFiguresCount)
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