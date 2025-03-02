using System;

public interface IFillable<out T>
{
     event Action<T> Filled;
}
