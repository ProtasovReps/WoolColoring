using Reflex.Attributes;
using UnityEngine;
using YG;
using Buffs;
using Buffs.Strategies;
using CustomInterface;

namespace PlayerGuide
{
    public class FreeBuffAdder : MonoBehaviour
    {
        [SerializeField] private int _freeBuffsCount = 1;

        private IBuff[] _buffs;
        private BuffBag _bag;

        [Inject]
        private void Inject(Unlocker unlocker, Filler filler, Breaker breaker, Remover remover, BuffBag bag)
        {
            _buffs = new IBuff[] { unlocker, filler, breaker, remover };
            _bag = bag;
        }

        private void Awake()
        {
            if (YG2.saves.IfFreeBuffsGiven)
                return;

            AddFreeBuffs();
        }

        private void AddFreeBuffs()
        {
            for (int i = 0; i < _buffs.Length; i++)
                _bag.AddBuff(_buffs[i], _freeBuffsCount);

            YG2.saves.IfFreeBuffsGiven = true;
        }
    }
}