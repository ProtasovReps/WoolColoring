using System;
using System.Collections.Generic;
using Interface;

namespace BuffSystem
{
    public class BuffBag
    {
        private readonly Dictionary<IBuff, int> _buffs;

        public BuffBag(Dictionary<IBuff, int> buffs)
        {
            if (buffs == null)
                throw new ArgumentNullException(nameof(buffs));

            _buffs = buffs;
        }

        public event Action AmountChanged;

        public IReadOnlyDictionary<IBuff, int> Buffs => _buffs;

        public void AddBuff(IBuff buff, int addCount)
        {
            ValidateBuff(buff);

            if (addCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(addCount));

            _buffs[buff] += addCount;
            AmountChanged?.Invoke();
        }

        public bool TryGetBuff(IBuff buff)
        {
            ValidateBuff(buff);

            if (_buffs[buff] == 0)
                return false;

            _buffs[buff]--;
            AmountChanged?.Invoke();
            return true;
        }

        private void ValidateBuff(IBuff buff)
        {
            if (_buffs.ContainsKey(buff) == false)
                throw new InvalidOperationException(nameof(buff));
        }
    }
}