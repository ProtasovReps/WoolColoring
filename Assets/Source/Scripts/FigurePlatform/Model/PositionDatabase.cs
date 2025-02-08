using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

public class PositionDatabase
{
    private readonly ConveyerPosition[] _positions;
    private ITransformable[] _transformables;

    public event Action<ITransformable, Vector3> PositionChanged;

    public PositionDatabase(IReadOnlyCollection<ConveyerPosition> positions)
    {
        if (positions == null)
            throw new ArgumentNullException();

        if (positions.Count == 0)
            throw new EmptyCollectionException();

        _transformables = new Figure[positions.Count];
        var tempPositions = new List<ConveyerPosition>(positions.Count);

        foreach (var position in positions)
            tempPositions.Add(position);

        _positions = tempPositions.OrderByDescending(position => position.Position.y).ToArray();
    }

    public int PositionsCount => _positions.Length;

    public void Add(ITransformable transformable)
    {
        int lastIndex = _transformables.Length - 1;
        ITransformable targetCell = _transformables[lastIndex];

        if (targetCell != null)
            throw new InvalidOperationException(nameof(transformable));

        _transformables[lastIndex] = transformable;

        Sort();
    }

    public void Remove(ITransformable transformableToRemove)
    {
        if (transformableToRemove == null)
            throw new ArgumentNullException(nameof(transformableToRemove));

        int searchedIndex = Array.FindIndex(_transformables, transformable => transformable == transformableToRemove);

        if (_transformables[searchedIndex] == null)
            throw new InvalidOperationException(nameof(transformableToRemove));

        _transformables[searchedIndex] = null;

        Sort();
    }

    private void Sort()
    {
        int needFeelCount = 0;

        for (int i = 0; i < _transformables.Length; i++)
        {
            if (_transformables[i] == null)
            {
                needFeelCount++;
            }
            else
            {
                ITransformable tempTransformable = _transformables[i];
                int emptyPositionIndex = i - needFeelCount;

                _transformables[i] = null;
                _transformables[emptyPositionIndex] = tempTransformable;

                PositionChanged?.Invoke(_transformables[emptyPositionIndex], _positions[emptyPositionIndex].Position);
            }
        }
    }
}