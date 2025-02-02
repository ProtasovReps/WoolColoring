using System;

public interface IFillable<T>
{
     event Action<T> Filled;
}
