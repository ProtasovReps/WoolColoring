using UnityEngine;

public class BoltView : MonoBehaviour
{
    [SerializeField] private ColorString _colorString;

    public IColorable ColorString => _colorString;
}
