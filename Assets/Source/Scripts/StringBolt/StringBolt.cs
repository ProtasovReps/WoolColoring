using UnityEngine;

public class StringBolt : MonoBehaviour
{
    [SerializeField] private ColorString _colorString;

    public IColorable ColorString => _colorString;
}
