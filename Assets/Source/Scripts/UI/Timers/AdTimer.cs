using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;
using YG;

namespace LevelInterface.Timers
{
    public class AdTimer : IDisposable
    {
        private readonly float _coolDownTime;
        private CancellationTokenSource _cancellationTokenSource;

        public AdTimer(float cooldownTime)
        {
            _coolDownTime = cooldownTime;
            ElapsedTime = YG2.saves.LastElapsedTimerTime;
            StartTimer().Forget();
        }

        public event Action TimeElapsed;
        public event Action Reseted;

        public float ElapsedTime { get; private set; }
        public float CooldownTime => _coolDownTime;
        public bool IsCounting { get; private set; }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            YG2.saves.LastElapsedTimerTime = ElapsedTime;
        }

        public void Reset()
        {
            ElapsedTime = 0f;
            StartTimer().Forget();
            Reseted?.Invoke();
        }

        private async UniTaskVoid StartTimer()
        {
            IsCounting = true;

            _cancellationTokenSource = new CancellationTokenSource();

            while (ElapsedTime < _coolDownTime)
            {
                ElapsedTime += Time.unscaledDeltaTime;
                await UniTask.Yield(_cancellationTokenSource.Token, true);
            }

            IsCounting = false;
            TimeElapsed?.Invoke();
        }
    }
}