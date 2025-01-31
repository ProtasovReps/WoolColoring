using UnityEngine;

public class StringBolt : MonoBehaviour
{
    [SerializeField] private ColorString _colorString;

    public IColorable GetString() => _colorString;
}
