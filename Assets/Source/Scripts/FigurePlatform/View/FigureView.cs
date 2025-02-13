using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ActiveStateSwitcher))]
[RequireComponent(typeof(TransformView))]
[RequireComponent(typeof(ColorView))]
public class FigureView : MonoBehaviour, IFallable, IColorSettable
{
    [SerializeField] private BoltContainer _boltContainer;
    [SerializeField] private Collider _collider;

    private FigurePresenter _presenter;
    private TransformView _transformView;
    private ColorView _colorView;
    private ActiveStateSwitcher _activeStateSwitcher;

    public Collider Collider => _collider;
    public IEnumerable<BoltView> Bolts => _boltContainer.Bolts;

    public void Initialize(FigurePresenter figurePresenter)
    {
        if (_boltContainer == null)
            throw new NullReferenceException(nameof(_boltContainer));

        if (figurePresenter == null)
            throw new ArgumentNullException(nameof(figurePresenter));

        _transformView = GetComponent<TransformView>();
        _activeStateSwitcher = GetComponent<ActiveStateSwitcher>();
        _colorView = GetComponent<ColorView>();
        _presenter = figurePresenter;

        _transformView.Initialize();
        _colorView.Initialize();
        _activeStateSwitcher.Initialize();
    }

    public void Appear()
    {
        _transformView.SetStartTransform();

        _activeStateSwitcher.SetActive(true);
    }

    public void Fall()
    {
        _activeStateSwitcher.SetActive(false);

        _presenter.Fall();
    }

    public void SetColor(Color color)
    {
        _colorView.SetColor(color);
    }
}