using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockHolderConnector : MonoBehaviour
{
    [SerializeField] private float _ropeDisconnectDelay;
    [SerializeField] private float _connectDelay = 0.5f;

    private ColoredStringHolderView[] _stringHolderViews;
    private RopePool _ropePool;
    private float _disconnectDelay;
    private Dictionary<Color, Rope> _connections;

    public void Setup(ColorBlockView block)
    {
        Color requiredColor = block.RequiredColor;
        ColoredStringHolderView holder = GetColoredHolder(requiredColor);

        if (holder.IsAnimating)
        {
            if (_connections.ContainsKey(requiredColor) == false)
            {
                ConnectDelayed(block, holder, requiredColor).Forget();
            }
        }
        else
        {
            SetupRope(block, holder, requiredColor);
        }
    }

    [Inject]
    private void Inject(ColoredStringHolderView[] stringHolderViews, RopePool ropePool)
    {
        _stringHolderViews = stringHolderViews;
        _ropePool = ropePool;
        _connections = new Dictionary<Color, Rope>();
        _disconnectDelay = _ropeDisconnectDelay;
    }

    private void SetupRope(ColorBlockView block, ColoredStringHolderView holder, Color color)
    {
        if (_connections.ContainsKey(color))
        {
            _connections[color].Reconnect(block.Transform).Forget();
        }
        else
        {
            Rope newRope = _ropePool.Get();

            newRope.SetColor(color);
            newRope.Connect(holder.Transform, block.Transform);
            _connections.Add(color, newRope);

            DisconnectDelayed(newRope).Forget();
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

    private async UniTaskVoid ConnectDelayed(ColorBlockView block, ColoredStringHolderView holder, Color color)
    {
        await UniTask.WaitForSeconds(_connectDelay);
        SetupRope(block, holder, color);
    }

    private async UniTaskVoid DisconnectDelayed(Rope rope)
    {
        await UniTask.WaitForSeconds(_disconnectDelay);
        rope.Disconnect().Forget();
        _connections.Remove(rope.Color);
    }
}