using UnityEngine;
using System;

public class FigureFactory : MonoBehaviour
{
    private BoltStash _stash;
    private FigureBinder _figureBinder;

    public void Initialize(BoltStash boltStash)
    {
        if (boltStash == null)
            throw new ArgumentNullException(nameof(boltStash));

        _stash = boltStash;

        _figureBinder = new FigureBinder();
    }

    public Figure Produce(FigureView view)
    {
        _stash.Add(view.Bolts);
        return BindFigure(view);
    }

    private Figure BindFigure(FigureView view)
    {
        var model = new Figure();
        var presenter = new FigurePresenter(model, view);

        return _figureBinder.Bind(model, view, presenter);
    }
}