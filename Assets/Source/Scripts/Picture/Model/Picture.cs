using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Picture
{
    private IReadOnlyCollection<ColorBlock> _colorBlocks;

    private Dictionary<Color, Queue<ColorBlock>> _requiredColors;

    public event Action<Color> ColorFilled;

    public void Initialize(IReadOnlyCollection<ColorBlock> colorBlocks)
    {
        if (colorBlocks == null)
            throw new NullReferenceException();

        if (colorBlocks.Count == 0)
            throw new EmptyCollectionException();

        _colorBlocks = colorBlocks;

        FillDictionary();
    }

    public Color GetRequiredColor()
    {
        int randomIndex = Random.Range(0, _requiredColors.Count);

        return _requiredColors.Keys.ToArray()[randomIndex];
    }

    public void Colorize(Color color)
    {
        if (_requiredColors.ContainsKey(color) == false)
            return;

        ColorBlock colorBlock = _requiredColors[color].Dequeue();

        colorBlock.SetColor(color);

        if (_requiredColors[color].Count == 0)
        {
            _requiredColors.Remove(color);
            ColorFilled?.Invoke(color);
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

        foreach (Queue<ColorBlock> queue in _requiredColors.Values)
        {
            queue.Shuffle();
        }
    }
}