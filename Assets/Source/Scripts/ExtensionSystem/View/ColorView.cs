using CustomInterface;
using UnityEngine;

namespace Extensions.View
{
    public class ColorView : MonoBehaviour, IColorSettable
    {
        private const string ColorMaterialProperty = "_Color";

        [SerializeField] private Renderer _renderer;

        private MaterialPropertyBlock _propertyBlock;

        public Color Color { get; private set; }

        public void Initialize()
        {
            _propertyBlock = new MaterialPropertyBlock();
        }

        public void SetColor(Color color)
        {
            Color = color;

            _propertyBlock.SetColor(ColorMaterialProperty, color);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}
