using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockHolderConnector : MonoBehaviour
{
    [SerializeField] private ColoredStringHolderView[] _stringHolderViews;
    [SerializeField] private ColorBlockViewStash _colorBlockStash;
    [SerializeField] private RopePool _ropePool;
    [SerializeField] private float _disconnectDelay;

    private Dictionary<Color, Rope> _connections;

    private void OnDestroy()
    {
        foreach (ColorBlockView colorBlock in _colorBlockStash.ColorBlockViews)
            colorBlock.Coloring -= OnColoring;
    }

    public void Initialize()
    {
        foreach (ColorBlockView colorBlock in _colorBlockStash.ColorBlockViews)
            colorBlock.Coloring += OnColoring;

        _connections = new Dictionary<Color, Rope>();
    }

    private void OnColoring(ColorBlockView blockView)
    {
        Color requiredColor = blockView.RequiredColor;

        if (_connections.ContainsKey(requiredColor) == false)
            ConnectBlock(requiredColor, blockView.Transform);
        else
            _connections[requiredColor].Reconnect(blockView.Transform);
    }

    private void ConnectBlock(Color requiredColor, Transform blockTransform)
    {
        Rope newRope = _ropePool.Get();
        Transform holderTransform = GetColoredHolder(requiredColor);

        newRope.Disconected += OnRopeDisconnected;
        newRope.SetColor(requiredColor);
        newRope.Connect(holderTransform, blockTransform);
        _connections.Add(requiredColor, newRope);
    }

    private Transform GetColoredHolder(Color color)
    {
        for (int i = 0; i < _stringHolderViews.Length; i++)
        {
            if (_stringHolderViews[i].Color == color)
                return _stringHolderViews[i].Transform;
        }

        throw new InvalidOperationException();
    }

    private void OnRopeDisconnected(Rope rope)
    {
        rope.Disconected -= OnRopeDisconnected;

        _connections.Remove(rope.Color);
    }
}
