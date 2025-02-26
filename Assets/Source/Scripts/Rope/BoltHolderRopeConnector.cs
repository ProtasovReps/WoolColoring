using System;
using System.Collections.Generic;
using UnityEngine;

public class BoltHolderRopeConnector : MonoBehaviour
{
    [SerializeField] private ColoredStringHolderView[] _stringHolderViews;
    [SerializeField] private WhiteStringHolderView _whiteStringHolder;
    [SerializeField] private RopePool _ropePool;

    private Dictionary<Bolt, Rope> _connectedPairs;
    private StringDistributor _stringDistributor;

    private void OnDestroy()
    {
        _stringDistributor.BoltDistributing -= SetRope;
    }

    public void Initialize(StringDistributor stringDistributor)
    {
        if (stringDistributor == null)
            throw new ArgumentNullException(nameof(stringDistributor));

        _stringDistributor = stringDistributor;
        _connectedPairs = new Dictionary<Bolt, Rope>();
        _stringDistributor.BoltDistributing += SetRope;
    }

    private void SetRope(Bolt boltView)
    {
        Color requiredColor = boltView.Colorable.Color;
        Transform holderString = GetFreeHolderString(requiredColor);
        Rope rope = _ropePool.Get();

        boltView.Disabling += DisconnectRope;

        _connectedPairs.Add(boltView, rope);
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

        _connectedPairs[boltView].Disconect();
        _connectedPairs.Remove(boltView);
    }
}
