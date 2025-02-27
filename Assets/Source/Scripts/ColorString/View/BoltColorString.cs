using UnityEngine;

[RequireComponent(typeof(ColorView))]
public class BoltColorString : MonoBehaviour, IColorSettable, IColorable
{
    private ColorView _colorView;

    public Color Color => _colorView.Color;

    public void Initialize()
    {
        _colorView = GetComponent<ColorView>();
        _colorView.Initialize();
    }

    public void SetColor(Color color)
    {
        _colorView.SetColor(color);
    }
}
