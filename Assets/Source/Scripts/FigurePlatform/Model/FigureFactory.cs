using Reflex.Attributes;
using UnityEngine;

public class FigureFactory : MonoBehaviour
{
    private BoltStash _stash;

    [Inject]
    private void Inject(BoltStash stash)
    {
        _stash = stash;
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
        view.Initialize(presenter);

        return model;
    }
}