using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using YG;

namespace YandexGamesSDK
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
