using YG;
using BuffSystem;
using BuffSystem.Strategies;
using Interface;

namespace Yandex.Saves.Buffs
{
    public class FillerSaver : BuffSaver
    {
        public FillerSaver(BuffBag bag)
            : base(bag)
        {
        }

        protected override void ValidateBuff(IBuff buff, int count)
        {
            if (buff is not Filler)
                return;

            YG2.saves.Fillers = count;
        }
    }
}