using UnityEngine;

public class ColoredStringHolderView : StringHolderView, IColorSettable
{
    [SerializeField] private ColorView _colorView;

    public override void Initialize(StringHolderPresenter presenter)
    {
        _colorView.Initialize();
        base.Initialize(presenter);
    }

    public void SetColor(Color color)
    {
        _colorView.SetColor(color);
    }
}
