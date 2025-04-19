using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using UnityEngine;

public class HoldersRopeConnector : MonoBehaviour
{
    [SerializeField] private float _ropeConnectDelay;
    [SerializeField] private float _perStringLifeTime;

    private WhiteStringHolderView _whiteStringHolderView;
    private ColoredStringHolderView[] _coloredHolders;
    private RopePool _ropePool;
    private StringDistributor _stringDistributor;

    [Inject]
    private void Inject(StringDistributor stringDistributor, ColoredStringHolderView[] coloredViews, WhiteStringHolderView whiteView, RopePool ropePool)
    {
        _stringDistributor = stringDistributor;
        _coloredHolders = coloredViews;
        _whiteStringHolderView = whiteView;
        _ropePool = ropePool;
        _stringDistributor.WhiteHolderDistributing += ConnectHolders;
    }

    private void OnDestroy()
    {
        _stringDistributor.WhiteHolderDistributing -= ConnectHolders;
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
        rope.Connect(_whiteStringHolderView.Transform, freePosition);

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