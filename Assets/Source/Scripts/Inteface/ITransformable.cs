using UnityEngine;

public interface ITransformable
{
    Vector3 Position { get; }

    public void SetPosition(Vector3 position);
}
