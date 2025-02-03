using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorBlockView : MonoBehaviour
{
    [SerializeField] private Color _requiredColor;

    private MeshRenderer _renderer;

    public Color RequiredColor => _requiredColor;

    private void Awake()
    {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void SetRenderColor(Color color)
    {
        _renderer.material.color = color;
    }
}
