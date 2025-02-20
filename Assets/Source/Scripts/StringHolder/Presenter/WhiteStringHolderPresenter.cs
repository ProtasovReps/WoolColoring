using System;

public class WhiteStringHolderPresenter
{
    private readonly WhiteStringHolderView _view;
    private readonly WhiteStringHolder _model;

    public WhiteStringHolderPresenter(WhiteStringHolderView view, WhiteStringHolder model)
    {
        if (view == null)
            throw new ArgumentNullException(nameof(view));

        if (model == null)
            throw new ArgumentNullException(nameof(model));

        _view = view;
        _model = model;
    }
}
