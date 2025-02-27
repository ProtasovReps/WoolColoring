using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockHolderConnector : MonoBehaviour
{
    [SerializeField] private ColoredStringHolderView[] _stringHolderViews;
    [SerializeField] private RopePool _ropePool;
    [SerializeField] private float _ropeDisconnectDelay;

    private WaitForSeconds _connectDelay;
    private WaitForSeconds _disconnectDelay;
    private Dictionary<Color, Rope> _connections;

    public void Initialize()
    {
        _connections = new Dictionary<Color, Rope>();
        _connectDelay = new WaitForSeconds(_stringHolderViews[0].SwitchDuration);
        _disconnectDelay = new WaitForSeconds(_ropeDisconnectDelay);
    }

    public void Setup(ColorBlockView block)
    {
        Color requiredColor = block.RequiredColor;
        ColoredStringHolderView holder = GetColoredHolder(requiredColor);

        if (holder.IsAnimating)
        {
            if (_connections.ContainsKey(requiredColor) == false)
            {
                StartCoroutine(ConnectDelayed(block, holder, requiredColor));
            }
        }
        else
        {
            SetupRope(block, holder, requiredColor);
        }
    }

    private void SetupRope(ColorBlockView block, ColoredStringHolderView holder, Color color)
    {
        if (_connections.ContainsKey(color))
        {
            _connections[color].Reconnect(block.Transform);
        }
        else
        {
            Rope newRope = _ropePool.Get();

            newRope.SetColor(color);
            newRope.Connect(holder.Transform, block.Transform);
            _connections.Add(color, newRope);

            StartCoroutine(DisconnectDelayed(newRope));
        }
    }

    private ColoredStringHolderView GetColoredHolder(Color color)
    {
        for (int i = 0; i < _stringHolderViews.Length; i++)
        {
            if (_stringHolderViews[i].Color == color)
                return _stringHolderViews[i];
        }

        throw new InvalidOperationException();
    }

    private IEnumerator ConnectDelayed(ColorBlockView block, ColoredStringHolderView holder, Color color)
    {
        yield return _connectDelay;
        SetupRope(block, holder, color);
    }

    private IEnumerator DisconnectDelayed(Rope rope)
    {
        yield return _disconnectDelay;
        rope.Disconnect();
        _connections.Remove(rope.Color);
    }
}