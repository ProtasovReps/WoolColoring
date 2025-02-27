using System;
using UnityEngine;

public class Malbert : MonoBehaviour
{
    [SerializeField] private Transform _upperBound;
    [SerializeField] private Transform _lowerBound;
    [SerializeField] private ColorBlockViewStash _blockStash;

    private PicturePresenter _picturePresenter;
    private Vector3 _upperBoundPosition;
    private Vector3 _lowerBoundPosition;

    public void Initilize(PicturePresenter picturePresenter)
    {
        if (picturePresenter == null)
            throw new ArgumentNullException(nameof(picturePresenter));

        _upperBoundPosition = _upperBound.position;
        _lowerBoundPosition = _lowerBound.position;
        _picturePresenter = picturePresenter;
    }

    private void OnEnable()
    {
        foreach (ColorBlockView colorBlockView in _blockStash.ColorBlockViews)
            colorBlockView.Coloring += OnColoring;
    }

    private void OnDisable()
    {
        foreach (ColorBlockView colorBlockView in _blockStash.ColorBlockViews)
            colorBlockView.Coloring -= OnColoring;
    }

    private void OnColoring(ColorBlockView colorBlockView)
    {
        if (colorBlockView.Transform.position.y > _upperBoundPosition.y)
            _picturePresenter.Move(colorBlockView.Transform, _upperBoundPosition);

        if (colorBlockView.Transform.position.y < _lowerBoundPosition.y)
            _picturePresenter.Move(colorBlockView.Transform, _lowerBoundPosition);
    }
}