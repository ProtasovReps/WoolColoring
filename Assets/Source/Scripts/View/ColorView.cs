using UnityEngine;

public class ColorView : MonoBehaviour, IColorSettable
{
    [SerializeField] private Renderer _renderer;

    private MaterialPropertyBlock _propertyBlock;

    public Color Color { get; private set; }

    public void Initialize() => _propertyBlock = new MaterialPropertyBlock();

    public void SetColor(Color color)
    {
        Color = color;

        _propertyBlock.SetColor(MaterialPropertyBlockParameters.Color, color);
        _renderer.SetPropertyBlock(_propertyBlock);
    }
}
