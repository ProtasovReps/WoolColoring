using System;
using UnityEngine;

public class FigureComposition : ITransformable, IUnsubscribable, IColorSettable
{
    private readonly Figure[] _figures;
    private int _falledCount;

    public event Action Appeared;
    public event Action PositionChanged;
    public event Action<FigureComposition> Emptied;

    public FigureComposition(Figure[] figures)
    {
        if (figures == null)
            throw new ArgumentNullException(nameof(figures));

        if (figures.Length == 0)
            throw new EmptyCollectionException();

        _figures = figures;

        Subscribe();
    }

    public Vector3 Position { get; private set; }

    public void SetPosition(Vector3 position)
    {
        Position = position;
        PositionChanged?.Invoke();
    }

    public void SetColor(Color color)
    {
        for (int i = 0; i < _figures.Length; i++)
            _figures[i].SetColor(color);
    }

    public void Appear()
    {
        EnableAllFigures();

        Appeared?.Invoke();
    }

    public void Unsubscribe()
    {
        for (int i = 0; i < _figures.Length; i++)
            _figures[i].Falled -= OnFigureFalled;
    }

    private void Subscribe()
    {
        for (int i = 0; i < _figures.Length; i++)
            _figures[i].Falled += OnFigureFalled;
    }

    private void EnableAllFigures()
    {
        for (int i = 0; i < _figures.Length; i++)
            _figures[i].Appear();
    }

    private void OnFigureFalled()
    {
        _falledCount++;

        if (_falledCount == _figures.Length)
        {
            Emptied?.Invoke(this);
            _falledCount = 0;
        }
    }
}