using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Picture : MonoBehaviour
{
    [SerializeField] private ColorBlock[] _colorBlocks;

    private Dictionary<Color, Queue<ColorBlock>> _requiredColors;

    public void Initialize()
    {
        if (_colorBlocks.Length == 0)
        {
            throw new EmptyCollectionException();
        }

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

    public Color GetRequiredColor()
        => _requiredColors.Keys.ToArray()[Random.Range(0, _requiredColors.Count)];

    public void Colorize(Color color)
    {
        if (_requiredColors.ContainsKey(color) == false)
            return;

        ColorBlock colorBlock = _requiredColors[color].Dequeue();

        colorBlock.SetColor(color);

        if (_requiredColors[color].Count == 0)
            _requiredColors.Remove(color);
    }
}