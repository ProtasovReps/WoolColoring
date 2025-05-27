using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using YG;

namespace Yandex
{
    public class SDKAwaiter : MonoBehaviour
    {
        public async UniTaskVoid WaitSDKInitialization(Action callback)
        {
            while (YG2.isSDKEnabled == false)
            {
                await UniTask.Yield();
            }

            callback?.Invoke();
        }
    }
}
