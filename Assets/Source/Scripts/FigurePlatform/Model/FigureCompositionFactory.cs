using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FigureCompositionFactory : MonoBehaviour
{
    [SerializeField] private FigureCompositionView[] _compositionViewsPrefabs;
    [SerializeField] private Color[] _figureColors;

    private FigureFactory _figureFactory;
    private FigureCompositionBinder _binder;
    private ColorPallete _colorPallete;
    private Queue<FigureCompositionView> _newFigures;
    private List<FigureCompositionView> _producedCompositions;

    public void Initialize(FigureFactory figureFactory)
    {
        if (figureFactory == null)
            throw new ArgumentNullException(nameof(figureFactory));

        var tempArray = new Color[_figureColors.Length];

        for (int i = 0; i < _figureColors.Length; i++)
            tempArray[i] = _figureColors[i];

        _figureFactory = figureFactory;
        _binder = new FigureCompositionBinder();
        _colorPallete = new ColorPallete(tempArray);
        _newFigures = new Queue<FigureCompositionView>(_compositionViewsPrefabs);
        _producedCompositions = new List<FigureCompositionView>(_compositionViewsPrefabs.Length);
    }

    public FigureComposition Produce()
    {
        FigureCompositionView compositionView;

        if (_newFigures.Count == 0)
        {
            int randomIndex = Random.Range(0, _producedCompositions.Count);
            compositionView = _producedCompositions[randomIndex];
        }
        else
        {
            compositionView = _newFigures.Dequeue();
            _producedCompositions.Add(compositionView);
        }

        FigureCompositionView newView = Instantiate(compositionView);
        Figure[] figures = GetFigures(newView);

        return BindComposition(newView, figures);
    }

    private Figure[] GetFigures(FigureCompositionView compositionView)
    {
        var figures = new List<Figure>();

        foreach (FigureView figureView in compositionView.FigureViews)
        {
            Figure newFigure = _figureFactory.Produce(figureView);

            figures.Add(newFigure);
        }

        return figures.ToArray();
    }

    private FigureComposition BindComposition(FigureCompositionView view, Figure[] figures)
    {
        var model = new FigureComposition(figures);
        var presenter = new FigureCompositionPresenter(model, view, _colorPallete);

        _binder.Bind(view, presenter);
        return model;
    }
}
