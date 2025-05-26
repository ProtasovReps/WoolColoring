using ClickReaders;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PlayerGuide
{
    public class BreakerBuffReplic : Replic
    {
        [SerializeField] private BoltClickReader _boltClickReader;
        [SerializeField] private FigureClickReader _figureClickReader;

        public override void Activate()
        {
            WaitExecution().Forget();
            base.Activate();
        }

        protected override void Deactivate()
        {
            _figureClickReader.SetPause(true);
            _boltClickReader.SetPause(true);
            base.Deactivate();
        }

        private async UniTaskVoid WaitExecution()
        {
            await UniTask.WaitUntil(() => _figureClickReader.IsPaused == true);
            Deactivate();
        }
    }
}