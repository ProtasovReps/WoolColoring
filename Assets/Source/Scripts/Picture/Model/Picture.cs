using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using ColorBlocks.Model;
using Extensions;
using CustomInterface;

namespace BlockPicture.Model
{
    public class Picture : IFillable<Color>
    {
        private readonly ColorBlock[] _colorBlocks;

        private Dictionary<Color, Queue<ColorBlock>> _requiredColors;

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

        public event Action<Color> Filled;
        public event Action BlockCountChanged;
        public event Action Finished;

        public int UncoloredBlocksCount { get; private set; }
        public int RequiredColorsCount => _requiredColors.Keys.Count;

        public bool GetRandomColor(out Color color)
        {
            if (_requiredColors.Keys.Count == 0)
            {
                Finished?.Invoke();
                color = default;
                return false;
            }

            int randomIndex = Random.Range(0, _requiredColors.Count);

            color = _requiredColors.Keys.ToArray()[randomIndex];
            return true;
        }

        public void Colorize(Color color)
        {
            if (_requiredColors.ContainsKey(color) == false)
            {
                return;
            }

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
}