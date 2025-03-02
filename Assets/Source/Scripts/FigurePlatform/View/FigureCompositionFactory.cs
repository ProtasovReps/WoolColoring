using Reflex.Attributes;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FigureCompositionFactory : MonoBehaviour
{
    [SerializeField] private FigureCompositionView[] _compositionViewsPrefabs;
    [SerializeField] private Color[] _figureColors;
    [SerializeField] private Transform _figureContainer;
    [SerializeField] private FigureFactory _figureFactory;

    private ColorPallete _colorPallete;
    private Queue<FigureCompositionView> _newFigures;
    private List<FigureCompositionView> _producedCompositions;

    [Inject]
    private void Initailize()
    {
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

        view.Initialize();
        view.Transform.SetParent(_figureContainer);
        return model;
    }
}
