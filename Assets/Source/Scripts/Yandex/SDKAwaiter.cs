using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using YG;

public class SDKAwaiter : MonoBehaviour
{
    public async UniTaskVoid WaitSDKInitialization(Action callback)
    {
        while(YG2.isSDKEnabled == false)
        {
            await UniTask.Yield();
        }

        callback?.Invoke();
    }
}
