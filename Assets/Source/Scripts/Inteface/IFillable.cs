using System;

namespace Interface
{
    public interface IFillable<out T>
    {
        event Action<T> Filled;
    }
}