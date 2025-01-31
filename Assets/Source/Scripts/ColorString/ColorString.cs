using UnityEngine;

public class ColorString : MonoBehaviour, IColorable
{
    [field: SerializeField] public Color Color { get; private set; }

    public void SetColor(Color color)
    {
        Color = color; //пока что без валидации
    }
}
