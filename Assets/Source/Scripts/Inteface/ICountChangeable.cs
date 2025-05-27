using System;

namespace Interface
{
    public interface ICountChangeable
    {
        event Action CountChanged;

        int Count { get; }
    }
}
