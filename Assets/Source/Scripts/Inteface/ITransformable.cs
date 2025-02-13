using System;
using UnityEngine;

public interface ITransformable
{
    event Action PositionChanged;

    Vector3 Position { get; }

    public void SetPosition(Vector3 position);
}