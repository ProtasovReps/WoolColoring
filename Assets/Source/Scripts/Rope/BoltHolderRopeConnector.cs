using Reflex.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BoltHolderRopeConnector : MonoBehaviour
{
    private ColoredStringHolderView[] _stringHolderViews;
    private WhiteStringHolderView _whiteStringHolder;
    private RopePool _ropePool;
    private Dictionary<Bolt, Rope> _connections;
    private StringDistributor _stringDistributor;

    private void OnDestroy()
    {
        _stringDistributor.BoltDistributing -= SetRope;
    }

    [Inject]
    private void Inject(StringDistributor stringDistributor, ColoredStringHolderView[] coloredViews, WhiteStringHolderView whiteView, RopePool ropePool)
    {
        if (stringDistributor == null)
            throw new ArgumentNullException(nameof(stringDistributor));

        _stringDistributor = stringDistributor;
        _stringHolderViews = coloredViews;
        _whiteStringHolder = whiteView;
        _ropePool = ropePool;
        _connections = new Dictionary<Bolt, Rope>();
        _stringDistributor.BoltDistributing += SetRope;
    }

    private void SetRope(Bolt boltView)
    {
        Color requiredColor = boltView.Colorable.Color;
        Transform holderString = GetFreeHolderString(requiredColor);
        Rope rope = _ropePool.Get();

        boltView.Disabling += DisconnectRope;

        _connections.Add(boltView, rope);
        rope.SetColor(requiredColor);
        rope.Connect(boltView.Transform, holderString);
    }

    private Transform GetFreeHolderString(Color requiredColor)
    {
        for (int i = 0; i < _stringHolderViews.Length; i++)
        {
            if (_stringHolderViews[i].Color == requiredColor)
            {
                if (_stringHolderViews[i].TryGetFreeStringTransform(out Transform transform) == false)
                    break;

                return transform;
            }
        }

        if (_whiteStringHolder.TryGetFreeStringTransform(out Transform whiteTransform) == false)
            throw new InvalidOperationException(nameof(whiteTransform));

        return whiteTransform;
    }

    private void DisconnectRope(Bolt boltView)
    {
        boltView.Disabling -= DisconnectRope;
        _connections[boltView].Disconnect().Forget();
        _connections.Remove(boltView);
    }
}
