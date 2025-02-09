using UnityEngine;

public interface IColorable : IColorSettable
{
    Color Color { get; }
}
