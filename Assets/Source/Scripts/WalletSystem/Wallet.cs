using Interface;
using System;

namespace WalletSystem
{
    public class Wallet : ICountChangeable
    {
        public Wallet(int startCount)
        {
            if (startCount < 0)
                throw new ArgumentOutOfRangeException(nameof(startCount));

            Count = startCount;
        }

        public event Action CountChanged;

        public int Count { get; private set; }

        public void Add(int count)
        {
            if (count <= 0)
                throw new ArgumentException(nameof(count));

            Count += count;
            CountChanged?.Invoke();
        }

        public void AddSilent(int count)
        {
            if (count <= 0)
                throw new ArgumentException(nameof(count));

            Count += count;
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
}