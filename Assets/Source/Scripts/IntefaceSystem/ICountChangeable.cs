using System;

namespace CustomInterface
{
    public interface ICountChangeable
    {
        event Action CountChanged;

        int Count { get; }
    }
}
