using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorBlockView : MonoBehaviour, IColorSettable
{
    [SerializeField] private Color _requiredColor;
    [SerializeField] private ColorView _colorView;

    public Color RequiredColor => _requiredColor;

    private void Awake()
    {
        _colorView.Initialize();
    }

    public void SetColor(Color color)
    {
        _colorView.SetColor(color);
    }
}
