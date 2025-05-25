using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PlayerGuide
{
    public class DefaultReplic : Replic
    {
        [SerializeField] private float _clickWaitDuration;
        [SerializeField] private ClickablePanel _clickablePanel;

        private CancellationTokenSource _cancellationToken;

        protected override void Deactivate()
        {
            _cancellationToken?.Cancel();
            _clickablePanel.Clicked -= Deactivate;

            base.Deactivate();
        }

        protected override void OnAnimationFinalized()
        {
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