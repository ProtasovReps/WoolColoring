using System;

public class Wallet : ICountChangeable
{
    public event Action CountChanged;

    public Wallet()
    {
        Count = 500;
    }

    public int Count {  get; private set; }

    public void Add(int count)
    {
        if (count <= 0)
            throw new ArgumentException(nameof(count));

        Count += count;
        CountChanged?.Invoke();
    }

    public bool TrySpend(int count)
    {
        if (Count < count)
            return false;

        Count -= count;
        CountChanged?.Invoke();
        return true;
    }
}