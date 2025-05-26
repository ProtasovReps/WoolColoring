using YG;
using Buffs;
using Buffs.Strategies;
using CustomInterface;

namespace YandexGamesSDK.Saves.Buffs
{
    public class UnlockerSaver : BuffSaver
    {
        public UnlockerSaver(BuffBag bag)
            : base(bag)
        {
        }

        protected override void ValidateBuff(IBuff buff, int count)
        {
            if (buff is not Unlocker)
                return;

            YG2.saves.Unlockers = count;
        }
    }
}