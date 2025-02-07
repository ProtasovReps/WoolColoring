using UnityEngine;

public class ConveyerPosition : MonoBehaviour
{
    public Vector3 Position { get; private set; }

    private void Awake() => Position = transform.position;
}
