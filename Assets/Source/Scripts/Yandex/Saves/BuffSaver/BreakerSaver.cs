using YG;
using Buffs;
using Buffs.Strategies;
using CustomInterface;

namespace YandexGamesSDK.Saves.Buffs
{
    public class BreakerSaver : BuffSaver
    {
        public BreakerSaver(BuffBag bag)
            : base(bag) { }

        protected override void ValidateBuff(IBuff buff, int count)
        {
            if (buff is not Breaker)
                return;

            YG2.saves.Breakers = count;
        }
    }
}