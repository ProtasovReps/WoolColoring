using System;

public interface IFillable<T>
{
    public event Action<T> Filled;
}
