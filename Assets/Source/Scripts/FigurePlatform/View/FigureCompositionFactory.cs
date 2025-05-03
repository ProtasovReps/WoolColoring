using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FigureCompositionFactory : MonoBehaviour
{
    [SerializeField] private FigureCompositionView[] _compositionViewsPrefabs;
    [SerializeField] private Color[] _figureColors;
    [SerializeField] private FigureFactory _figureFactory;

    private ColorPallete _colorPallete;
    private Queue<FigureCompositionView> _newFigures;
    private List<FigureCompositionView> _producedCompositions;
    private ObjectDisposer _disposer;

    public event Action<FigureComposition> Produced;

    public void Initialize(ObjectDisposer objectDisposer)
    {
        _disposer = objectDisposer;
        _figureFactory.Initialize(_disposer);
        _colorPallete = new ColorPallete(_figureColors);
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

        _disposer.Add(model);
        _disposer.Add(presenter);

        view.Initialize();
        view.Transform.SetParent(transform);

        Produced?.Invoke(model);
        return model;
    }
}