using System;
using UnityEngine;

public class Figure : ITransformable
{
    public event Action PositionChanged;
    public event Action Appeared;
    public event Action<Figure> Falled;

    public Vector3 Position { get; private set; }

    public void SetPosition(Vector3 position)
    {
        Position = position;
        PositionChanged?.Invoke();
    }

    public void Appear()
    {
        Appeared?.Invoke();
    }

    public void Fall()
    {
        Falled?.Invoke(this);
    }
}