using UnityEngine;
using Reflex.Attributes;

public class FigureFactory : MonoBehaviour
{
    [Inject] private readonly BoltStash _stash;

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