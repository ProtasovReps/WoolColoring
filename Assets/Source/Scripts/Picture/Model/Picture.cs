using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Picture : IFillable<Color>
{
    private readonly ColorBlock[] _colorBlocks;
    private Dictionary<Color, Queue<ColorBlock>> _requiredColors;

    public event Action<Color> Filled;
    public event Action BlockCountChanged;

    public Picture(ColorBlock[] colorBlocks)
    {
        if (colorBlocks == null)
            throw new NullReferenceException();

        if (colorBlocks.Length == 0)
            throw new EmptyCollectionException();

        _colorBlocks = colorBlocks;
        UncoloredBlocksCount = _colorBlocks.Length;

        FillDictionary();
    }

    public int UncoloredBlocksCount { get; private set; }
    public int RequiredColorsCount => _requiredColors.Keys.Count;

    public Color GetRandomColor()
    {
        if (_requiredColors.Keys.Count == 0)
            throw new InvalidOperationException();

        int randomIndex = Random.Range(0, _requiredColors.Count);
        return _requiredColors.Keys.ToArray()[randomIndex];
    }

    public void Colorize(Color color)
    {
        if (_requiredColors.ContainsKey(color) == false)
            return;

        ColorBlock colorBlock = _requiredColors[color].Dequeue();

        colorBlock.SetColor(color);

        UncoloredBlocksCount--;
        BlockCountChanged?.Invoke();

        if (_requiredColors[color].Count == 0)
        {
            _requiredColors.Remove(color);
            Filled?.Invoke(color);
        }
    }

    private void FillDictionary()
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
    }
}