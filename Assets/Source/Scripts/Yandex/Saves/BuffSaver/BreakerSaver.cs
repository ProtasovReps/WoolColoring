using YG;
using BuffSystem;
using BuffSystem.Strategies;
using Interface;

namespace Yandex.Saves.Buffs
{
    public class BreakerSaver : BuffSaver
    {
        public BreakerSaver(BuffBag bag)
            : base(bag)
        {
        }

        protected override void ValidateBuff(IBuff buff, int count)
        {
            if (buff is not Breaker)
                return;

            YG2.saves.Breakers = count;
        }
    }
}