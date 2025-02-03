using UnityEngine;

public class ColorString : MonoBehaviour, IColorable
{
    [SerializeField] private MeshRenderer _meshRenderer;

    public Color Color => _meshRenderer.material.color;

    public void SetColor(Color color) => _meshRenderer.material.color = color;
}
