using System;
using System.Collections;
using UnityEngine;

public class HoldersRopeConnector : MonoBehaviour
{
    [SerializeField] private ColoredStringHolderView[] _coloredHolders;
    [SerializeField] private float _ropeConnectDelay;
    [SerializeField] private float _perStringLifeTime;
    [SerializeField] private WhiteStringHolderView _whiteHolder;
    [SerializeField] private RopePool _ropePool;

    private StringDistributor _stringDistributor;
    private WaitForSeconds _delay;
    private WaitForSeconds _connectDelay;

    private void OnDestroy()
    {
        _stringDistributor.WhiteHolderDistributing -= ConnectHolders;
    }

    public void Initialize(StringDistributor stringDistributor)
    {
        if (stringDistributor == null)
            throw new ArgumentNullException(nameof(stringDistributor));

        _connectDelay = new WaitForSeconds(_ropeConnectDelay);
        _delay = new WaitForSeconds(_perStringLifeTime);
        _stringDistributor = stringDistributor;
        _stringDistributor.WhiteHolderDistributing += ConnectHolders;
    }

    private void ConnectHolders(Color color, int stringFillCount)
    {
        Transform freePosition = GetHolderTransform(color);

        if (freePosition == null)
            return;

        StartCoroutine(ConnectDelayed(color, freePosition, stringFillCount));
    }

    private IEnumerator ConnectDelayed(Color color, Transform freePosition, int stringFillCount)
    {
        yield return _connectDelay;
        Rope rope = _ropePool.Get();

        rope.SetColor(color);
        rope.Connect(_whiteHolder.Transform, freePosition);

        for (int i = 0; i < stringFillCount; i++)
            yield return _delay;

        rope.Disconnect();
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
