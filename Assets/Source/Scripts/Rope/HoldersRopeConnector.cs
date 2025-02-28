using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using System;
using UnityEngine;

public class HoldersRopeConnector : MonoBehaviour
{
    [SerializeField] private float _ropeConnectDelay;
    [SerializeField] private float _perStringLifeTime;

    [Inject] private readonly WhiteStringHolderView _whiteHolder;
    [Inject] private readonly ColoredStringHolderView[] _coloredHolders;
    [Inject] private readonly RopePool _ropePool;
    private StringDistributor _stringDistributor;

    private void OnDestroy()
    {
        _stringDistributor.WhiteHolderDistributing -= ConnectHolders;
    }

    public void Initialize(StringDistributor stringDistributor)
    {
        if (stringDistributor == null)
            throw new ArgumentNullException(nameof(stringDistributor));

        _stringDistributor = stringDistributor;
        _stringDistributor.WhiteHolderDistributing += ConnectHolders;
    }

    private void ConnectHolders(Color color, int stringFillCount)
    {
        Transform freePosition = GetHolderTransform(color);

        if (freePosition == null)
            return;

        ConnectDelayed(color, freePosition, stringFillCount).Forget();
    }

    private async UniTaskVoid ConnectDelayed(Color color, Transform freePosition, int stringFillCount)
    {
        await UniTask.WaitForSeconds(_ropeConnectDelay);
        Rope rope = _ropePool.Get();

        rope.SetColor(color);
        rope.Connect(_whiteHolder.Transform, freePosition);

        for (int i = 0; i < stringFillCount; i++)
            await UniTask.WaitForSeconds(_perStringLifeTime);

        rope.Disconnect().Forget();
    }

    private Transform GetHolderTransform(Color color)
    {
        Transform stringPosition = null;

        for (int i = 0; i < _coloredHolders.Length; i++)
        {
            if (_coloredHolders[i].Color != color)
                continue;

            stringPosition = _coloredHolders[i].Transform;
        }

        return stringPosition;
    }
}