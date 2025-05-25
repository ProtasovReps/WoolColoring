using System;

namespace CustomInterface
{
    public interface IFillable<out T>
    {
        event Action<T> Filled;
    }
}