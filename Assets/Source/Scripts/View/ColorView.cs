using UnityEngine;

public class ColorView : MonoBehaviour, IColorSettable
{
    [SerializeField] private MeshRenderer _renderer;

    private MaterialPropertyBlock _propertyBlock;

    public void Initialize()
    {
        _propertyBlock = new MaterialPropertyBlock();
    }

    public void SetColor(Color color)
    {
        _propertyBlock.SetColor(MaterialPropertyBlockParameters.Color, color);
        _renderer.SetPropertyBlock(_propertyBlock);
    }
}
