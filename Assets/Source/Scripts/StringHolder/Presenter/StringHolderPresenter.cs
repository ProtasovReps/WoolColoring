using System;

public class StringHolderPresenter : IEventListener
{
    private readonly StringHolderView _view;
    private readonly StringHolder _model;

    public StringHolderPresenter(StringHolderView view, StringHolder model)
    {
        if (view == null)
            throw new ArgumentNullException(nameof(view));

        if (model == null)
            throw new ArgumentNullException(nameof(model));

        _view = view;
        _model = model;
    }

    public virtual void Subscribe()
    {
        _model.StringAdded += OnStringAdded;
        _model.StringRemoved += OnStringRemoved;
    }

    public virtual void Unsubscribe()
    {
        _model.StringAdded -= OnStringAdded;
        _model.StringRemoved -= OnStringRemoved;
    }

    private void OnStringAdded(ColorString colorString)
    {
        _view.AddString(colorString);
    }

    private void OnStringRemoved(ColorString colorString)
    {
        _view.RemoveString(colorString);
    }
}
