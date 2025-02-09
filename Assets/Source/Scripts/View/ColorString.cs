using UnityEngine;

public class ColorString : MonoBehaviour, IColorable, IColorSettable
{
    [SerializeField] private ColorView _colorView;

    public Color Color { get; private set; }

    public void Initialize()
    {
        _colorView.Initialize();
    }

    public void SetColor(Color color)
    {
        Color = color;
        _colorView.SetColor(color);
    }
}
