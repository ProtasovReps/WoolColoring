using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigureView : MonoBehaviour, IFallable, IColorSettable
{
    [SerializeField] private BoltContainer _boltContainer;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Collider _collider;
    [SerializeField] private MeshRenderer _renderer;

    private MaterialPropertyBlock _propertyBlock;
    private FigurePresenter _presenter;
    private Coroutine _coroutine;
    private Transform _transform;
    private Vector3 _startPosition;
    private Quaternion _startRotation;

    public IEnumerable<BoltView> Bolts => _boltContainer.Bolts;

    public void Initialize(FigurePresenter figurePresenter)
    {
        if (_boltContainer == null)
            throw new NullReferenceException(nameof(_boltContainer));

        if (_collider == null)
            throw new NullReferenceException(nameof(_collider));

        if (figurePresenter == null)
            throw new ArgumentNullException(nameof(figurePresenter));

        _propertyBlock = new MaterialPropertyBlock();
        _transform = transform;
        _startRotation = _transform.rotation;
        _startPosition = _transform.position;
        _presenter = figurePresenter;
    }

    public void SetColor(Color color)
    {
        _propertyBlock.SetColor(MaterialPropertyBlockParameters.Color, color);
        _renderer.SetPropertyBlock(_propertyBlock);
    }

    public void Appear()
    {
        SetActive(true);
    }

    public void Fall()
    {
        _transform.position = _startPosition;
        _transform.rotation = _startRotation;

        SetActive(false);
        _presenter.Fall();
    }

    public void ChangePosition(Vector3 position)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(MoveSmoothly(position));
    }

    private void SetActive(bool isActive)
    {
        _transform.gameObject.SetActive(isActive);
    }

    private IEnumerator MoveSmoothly(Vector3 position)
    {
        float minDistance = 0.01f;

        _collider.enabled = false;

        while (GetSquareMagnitude(position) > minDistance)
        {
            _transform.position = Vector3.Lerp(_transform.position, position, _moveSpeed * Time.deltaTime);
            yield return null;
        }

        _collider.enabled = true;
        _coroutine = null;
    }

    private float GetSquareMagnitude(Vector3 position)
    {
        Vector3 offset = position - _transform.position;
        return Vector3.SqrMagnitude(offset);
    }
}
