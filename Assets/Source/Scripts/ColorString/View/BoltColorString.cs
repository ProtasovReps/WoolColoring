using UnityEngine;

[RequireComponent(typeof(ColorView))]
public class BoltColorString : MonoBehaviour, IColorSettable, IColorable
{
    private ColorView _colorView;

    public Color Color { get; private set; }

    public void Initialize()
    {
        _colorView = GetComponent<ColorView>();
        _colorView.Initialize();
    }

    public void SetColor(Color color)
    {
        Color = color;
        _colorView.SetColor(color);
    }
}
