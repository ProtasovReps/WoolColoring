using System;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using FigurePlatformSystem.View;
using Interface;

namespace FigurePlatformSystem.Model
{
    public class PositionDatabase
    {
        private readonly ConveyerPosition[] _positions;

        private IPositionSettable[] _positionSettables;

        public PositionDatabase(ConveyerPosition[] positions)
        {
            if (positions == null)
                throw new ArgumentNullException();

            if (positions.Length == 0)
                throw new EmptyCollectionException();

            _positionSettables = new FigureComposition[positions.Length];
            var tempPositions = new List<ConveyerPosition>(positions.Length);

            foreach (var position in positions)
                tempPositions.Add(position);

            _positions = tempPositions.ToArray();
        }

        public event Action<IPositionSettable, Vector3> PositionChanged;

        public int TransformablesCount { get; private set; }
        public int PositionsCount => _positions.Length;

        public void Add(IPositionSettable transformable)
        {
            int lastIndex = _positionSettables.Length - 1;
            IPositionSettable targetCell = _positionSettables[lastIndex];

            if (targetCell != null)
                throw new InvalidOperationException(nameof(transformable));

            _positionSettables[lastIndex] = transformable;
            TransformablesCount++;

            Sort();
        }

        public void Remove(IPositionSettable transformableToRemove)
        {
            if (transformableToRemove == null)
                throw new ArgumentNullException(nameof(transformableToRemove));

            int searchedIndex = Array.FindIndex(_positionSettables, transformable => transformable == transformableToRemove);

            if (_positionSettables[searchedIndex] == null)
                throw new InvalidOperationException(nameof(transformableToRemove));

            _positionSettables[searchedIndex] = null;
            TransformablesCount--;

            Sort();
        }

        private void Sort()
        {
            int needFeelCount = 0;

            for (int i = 0; i < _positionSettables.Length; i++)
            {
                if (_positionSettables[i] == null)
                {
                    needFeelCount++;
                }
                else
                {
                    IPositionSettable tempTransformable = _positionSettables[i];
                    int emptyPositionIndex = i - needFeelCount;

                    _positionSettables[i] = null;
                    _positionSettables[emptyPositionIndex] = tempTransformable;

                    PositionChanged?.Invoke(_positionSettables[emptyPositionIndex], _positions[emptyPositionIndex].Position);
                }
            }
        }
    }
}