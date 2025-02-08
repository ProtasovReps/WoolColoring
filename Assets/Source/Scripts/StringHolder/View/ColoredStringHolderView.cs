using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColoredStringHolderView : StringHolderView
{
    private MeshRenderer _meshRenderer;
    private MaterialPropertyBlock _propertyBlock;

    public override void Initialize(StringHolderPresenter presenter)
    {
        _propertyBlock = new MaterialPropertyBlock();
        _meshRenderer = GetComponent<MeshRenderer>();
        base.Initialize(presenter);
    }

    public void SetColor(Color color)
    {
        _propertyBlock.SetColor(MaterialPropertyBlockParameters.Color, color);
        _meshRenderer.SetPropertyBlock(_propertyBlock);
    }
}
