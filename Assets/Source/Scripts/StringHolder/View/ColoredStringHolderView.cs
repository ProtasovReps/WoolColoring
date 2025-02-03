using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColoredStringHolderView : StringHolderView
{
    private MeshRenderer _meshRenderer;

    public override void Initialize(StringHolderPresenter presenter)
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        base.Initialize(presenter);
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }
}
