using YG;
using Buffs;
using Buffs.Strategies;
using CustomInterface;

namespace YandexGamesSDK.Saves.Buffs
{
    public class RemoverSaver : BuffSaver
    {
        public RemoverSaver(BuffBag bag)
            : base(bag)
        {

        }

        protected override void ValidateBuff(IBuff buff, int count)
        {
            if (buff is not Remover)
                return;

            YG2.saves.Removers = count;
        }
    }
}