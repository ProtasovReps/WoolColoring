using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class FigureFactory : MonoBehaviour
{
    [SerializeField] private FigureView[] _viewPrefabs;

    private BoltStash _stash;
    private FigureBinder _figureBinder;
    private Queue<FigureView> _newFigures;
    private List<FigureView> _producedFigures;

    public void Initialize(BoltStash boltStash)
    {
        if (boltStash == null)
            throw new ArgumentNullException(nameof(boltStash));

        _stash = boltStash;
        _figureBinder = new FigureBinder();
        _newFigures = new Queue<FigureView>(_viewPrefabs);
        _producedFigures = new List<FigureView>(_viewPrefabs.Length);
    }

    public Figure Produce()
    {
        FigureView view;

        if (_newFigures.Count == 0)
        {
            int randomIndex = Random.Range(0, _producedFigures.Count);
            view = _producedFigures[randomIndex];
        }
        else
        {
            view = _newFigures.Dequeue();
            _producedFigures.Add(view);
        }

        view = Instantiate(view);
        return BindFigure(view);
    }

    private Figure BindFigure(FigureView view)
    {
        _stash.Add(view.Bolts);
        var model = new Figure();
        var presenter = new FigurePresenter(model, view);

        return _figureBinder.Bind(model, view, presenter);
    }
}