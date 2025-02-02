using System;
using System.Collections.Generic;
using UnityEngine;

public class Picture : MonoBehaviour
{
    [SerializeField] private ColorBlock[] _colorBlocks;

    private Dictionary<Color, Queue<ColorBlock>> _requiredColors;

    private void Awake()
    {
        _requiredColors = new Dictionary<Color, Queue<ColorBlock>>();

        foreach (ColorBlock colorBlock in _colorBlocks)
        {
            Color requiredColor = colorBlock.RequiredColor;

            if (_requiredColors.ContainsKey(requiredColor) == false)
            {
                _requiredColors.Add(requiredColor, new Queue<ColorBlock>());
            }

            _requiredColors[requiredColor].Enqueue(colorBlock);
        }

        foreach (Queue<ColorBlock> queue in _requiredColors.Values)
        {
            queue.Shuffle();
        }
    }

    public void Colorize(Color color)
    {
        if (_requiredColors.ContainsKey(color) == false)
            throw new ArgumentException(nameof(color));

        if (_requiredColors[color].Count == 0)
            return;

        ColorBlock colorBlock = _requiredColors[color].Dequeue();

        colorBlock.SetColor(color);
    }
}