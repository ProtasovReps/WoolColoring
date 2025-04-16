using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class TemporaryActivatableUI : ActivatableUI
{
    [SerializeField] private float _disappearDelay;

    private CancellationTokenSource _tokenSource;

    private void OnDestroy() => _tokenSource?.Cancel();

    public override void Activate()
    {
        base.Activate();
        ValidateTask();
        DeactivateDelayed().Forget();
    }

    private async UniTaskVoid DeactivateDelayed()
    {
        _tokenSource = new CancellationTokenSource();
        await UniTask.WaitForSeconds(_disappearDelay, true, cancellationToken: _tokenSource.Token, cancelImmediately: true);
        Deactivate();
    }

    private void ValidateTask()
    {
        if (_tokenSource == null)
            return;

        if (_tokenSource.IsCancellationRequested)
            return;

        _tokenSource.Cancel();
    }
}
