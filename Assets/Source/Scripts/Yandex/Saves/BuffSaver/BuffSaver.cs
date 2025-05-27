using System;
using BuffSystem;
using Interface;

namespace Yandex.Saves.Buffs
{
    public abstract class BuffSaver : ISaver
    {
        private readonly BuffBag _bag;

        public BuffSaver(BuffBag bag)
        {
            if (bag == null)
                throw new ArgumentNullException(nameof(bag));

            _bag = bag;
        }

        public void Save()
        {
            foreach (var buff in _bag.Buffs)
                ValidateBuff(buff.Key, buff.Value);
        }

        protected abstract void ValidateBuff(IBuff buff, int count);
    }
}