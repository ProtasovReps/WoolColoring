using System;
using YG;

public class Wallet : ICountChangeable
{
    public event Action CountChanged;

    public Wallet(int startCount)
    {
        if (startCount < 0)
            throw new ArgumentOutOfRangeException(nameof(startCount));

        Count = startCount;
    }

    public int Count { get; private set; }

    public void Add(int count)
    {
        if (count <= 0)
            throw new ArgumentException(nameof(count));

        Count += count;
        SaveCoins();

        CountChanged?.Invoke();
    }

    public bool TrySpend(int count)
    {
        if (Count < count)
            return false;

        Count -= count;
        SaveCoins();

        CountChanged?.Invoke();
        return true;
    }

    private void SaveCoins()
    {
        YG2.saves.Coins = Count;
        YG2.SaveProgress();
    }
}