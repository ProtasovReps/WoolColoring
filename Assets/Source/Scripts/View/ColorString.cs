using UnityEngine;

public class ColorString : MonoBehaviour, IColorable
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private MaterialPropertyBlock _propertyBlock;

    public Color Color { get; private set; }

    public void SetColor(Color color)
    {
        if (_propertyBlock == null)
            _propertyBlock = new MaterialPropertyBlock();

        Color = color;

        _propertyBlock.SetColor(MaterialPropertyBlockParameters.Color, color);
        _meshRenderer.SetPropertyBlock(_propertyBlock);
    }
}
