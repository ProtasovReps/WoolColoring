using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureView : MonoBehaviour, IFallable
{
    [SerializeField] private BoltContainer _boltContainer;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Collider _collider;

    private Transform _transform;
    private FigurePresenter _presenter;
    private Coroutine _coroutine;

    public IReadOnlyCollection<BoltView> Bolts => _boltContainer.Bolts;

    public void Initialize(FigurePresenter figurePresenter)
    {
        if (_boltContainer == null)
            throw new NullReferenceException(nameof(_boltContainer));

        if (_collider == null)
            throw new NullReferenceException(nameof(_collider));

        if (figurePresenter == null)
            throw new ArgumentNullException(nameof(figurePresenter));

        _transform = transform;
        _presenter = figurePresenter;
    }

    public void ChangePosition(Vector3 position)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(MoveSmoothly(position));
    }

    public void Appear()
    {
        _transform.position = Vector3.zero;
        _transform.gameObject.SetActive(true);
    }

    public void Fall()
    {
        _presenter.Fall();
        _transform.gameObject.SetActive(false);
    }

    private IEnumerator MoveSmoothly(Vector3 position)
    {
        _collider.enabled = false;

        while (_transform.position != position)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, position, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        _collider.enabled = true;
        _coroutine = null;
    }
}
