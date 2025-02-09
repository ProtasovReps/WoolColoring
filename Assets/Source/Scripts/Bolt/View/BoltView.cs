using UnityEngine;

public class BoltView : MonoBehaviour
{
    [SerializeField] private ColorString _colorString;

    public IColorSettable ColorSettable => _colorString;
    public IColorable Colorable => _colorString;

    private void Awake() => _colorString.Initialize();
}
