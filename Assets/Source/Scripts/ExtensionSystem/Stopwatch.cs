using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Extensions
{
    public class Stopwatch : IDisposable
    {
        private CancellationTokenSource _cancellationTokenSource;

        public float ElapsedTime { get; private set; }

        public void Dispose()
        {
            ElapsedTime = 0f;
        }

        public async UniTaskVoid StartCount()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            while (_cancellationTokenSource.IsCancellationRequested == false)
            {
                ElapsedTime += Time.deltaTime;
                await UniTask.Yield();
            }
        }

        public void Stop()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}