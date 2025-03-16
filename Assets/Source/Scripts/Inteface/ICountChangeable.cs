using System;

public interface ICountChangeable
{
    event Action CountChanged;

    int Count { get; }
}