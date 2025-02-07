using System.Collections.Generic;
using System;

public class FigurePool
{
    private readonly FigureFactory _factory;
    private readonly Queue<Figure> _disabledFigures;

    public FigurePool(FigureFactory factory)
    {
        if (factory == null)
            throw new ArgumentNullException(nameof(factory));

        _factory = factory;
        _disabledFigures = new Queue<Figure>();
    }

    public Figure Get()
    {
        Figure figure;

        if (_disabledFigures.Count == 0)
            _disabledFigures.Enqueue(Create());

        figure = _disabledFigures.Dequeue();
        figure.Appear();

        return figure;
    }

    public void Release(Figure figure)
    {
        _disabledFigures.Enqueue(figure);
    }

    private Figure Create() => _factory.Produce();
}
