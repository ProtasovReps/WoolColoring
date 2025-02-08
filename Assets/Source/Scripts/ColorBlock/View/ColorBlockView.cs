using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorBlockView : MonoBehaviour
{
    [SerializeField] private Color _requiredColor;

    private MeshRenderer _renderer;
    private MaterialPropertyBlock _materialPropertyBlock;

    public Color RequiredColor => _requiredColor;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
        _materialPropertyBlock = new MaterialPropertyBlock();
    }

    public void SetRenderColor(Color color)
    {
        _materialPropertyBlock.SetColor(MaterialPropertyBlockParameters.Color, color);
        _renderer.SetPropertyBlock(_materialPropertyBlock);
    }
}
