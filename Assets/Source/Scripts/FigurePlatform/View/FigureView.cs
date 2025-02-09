using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TransformView))]
[RequireComponent(typeof(ColorView))]
public class FigureView : MonoBehaviour, IFallable, IColorSettable
{
    [SerializeField] private BoltContainer _boltContainer;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Collider _collider;

    private FigurePresenter _presenter;
    private ColorView _colorView;
    private TransformView _transformView;

    public IEnumerable<BoltView> Bolts => _boltContainer.Bolts;

    public void Initialize(FigurePresenter figurePresenter)
    {
        if (_boltContainer == null)
            throw new NullReferenceException(nameof(_boltContainer));

        if (_collider == null)
            throw new NullReferenceException(nameof(_collider));

        if (figurePresenter == null)
            throw new ArgumentNullException(nameof(figurePresenter));

        _transformView = GetComponent<TransformView>();
        _colorView = GetComponent<ColorView>();
        _presenter = figurePresenter;

        _colorView.Initialize();
        _transformView.Initialize(_collider);
    }

    public void Appear()
    {
        _transformView.SetActive(true);
    }

    public void Fall()
    {
        _transformView.SetStartTransform();
        _transformView.SetActive(false);

        _presenter.Fall();
    }

    public void SetColor(Color color)
    {
        _colorView.SetColor(color);
    }

    public void ChangePosition(Vector3 position)
    {
        _transformView.ChangePosition(position, _moveSpeed);
    }
}
