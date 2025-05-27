using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GuideSystem
{
    public class DefaultReplic : Replic
    {
        [SerializeField] private float _clickWaitDuration;
        [SerializeField] private ClickablePanel _clickablePanel;

        private CancellationTokenSource _cancellationToken;

        public override void Activate()
        {
            ReplicAnimation.Finalized += OnAnimationFinalized;
            base.Activate();
        }

        protected override void Deactivate()
        {
            _cancellationToken?.Cancel();
            _clickablePanel.Clicked -= Deactivate;
            base.Deactivate();
        }

        protected virtual void OnAnimationFinalized()
        {
            ReplicAnimation.Finalized -= OnAnimationFinalized;
            WaitClick().Forget();
        }

        private async UniTaskVoid WaitClick()
        {
            _cancellationToken = new CancellationTokenSource();
            _clickablePanel.Clicked += Deactivate;

            await UniTask.WaitForSeconds(_clickWaitDuration, cancellationToken: _cancellationToken.Token, cancelImmediately: true);

            Deactivate();
        }
    }
}