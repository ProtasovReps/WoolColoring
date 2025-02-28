using UnityEngine;
using System;

public class Conveyer : IDisposable
{
    private readonly FigureCompositionPool _figurePool;
    private readonly PositionDatabase _positionDatabase;
    private readonly int _minFiguresCount = 3;

    public Conveyer(FigureCompositionPool pool, PositionDatabase positionDatabase)
    {
        _figurePool = pool;
        _positionDatabase = positionDatabase;

        _positionDatabase.PositionChanged += OnPositionChanged;
    }

    public void FillAllFigures()
    {
        while (_positionDatabase.PositionsCount > _positionDatabase.TransformablesCount)
            AddFigure();
    }

    public void Dispose()
    {
        _positionDatabase.PositionChanged -= OnPositionChanged;
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

    private void OnPositionChanged(ITransformable transformable, Vector3 position)
    {
        transformable.SetPosition(position);
    }
}