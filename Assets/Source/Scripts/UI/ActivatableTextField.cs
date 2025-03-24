using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class ActivatableTextField : ActivatableUI
{
    [SerializeField] private float _disappearTime;

    private CancellationTokenSource _tokenSource;

    public override void Activate()
    {
        base.Activate();
        ValidateTask();
        DeactivateDelayed().Forget();
    }

    private async UniTaskVoid DeactivateDelayed()
    {
        _tokenSource = new CancellationTokenSource();
        await UniTask.WaitForSeconds(_disappearTime, cancellationToken: _tokenSource.Token, cancelImmediately: true);
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
