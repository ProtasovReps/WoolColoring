using UnityEngine;

public class ColoredStringHolderView : StringHolderView, IColorSettable
{
    [SerializeField] private ColorView _colorView;

    public override void Initialize()
    {
        _colorView.Initialize();
        base.Initialize();
    }

    public void SetColor(Color color) => _colorView.SetColor(color);
}
