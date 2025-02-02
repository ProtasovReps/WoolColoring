using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
public class ColorString : MonoBehaviour, IColorable
{
    private MeshRenderer _meshRenderer;

    public void Initialize() => _meshRenderer = GetComponent<MeshRenderer>();

    public void SetColor(Color color) => _meshRenderer.material.color = color;

    public Color GetColor() => _meshRenderer.material.color;
}
