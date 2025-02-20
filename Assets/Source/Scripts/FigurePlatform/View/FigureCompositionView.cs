using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TransformMoveView))]
[RequireComponent(typeof(ActiveStateSwitcher))]
public class FigureCompositionView : MonoBehaviour
{
    [SerializeField] private FigureView[] _figureViews;
    [SerializeField] private float _moveSpeed;

    private Collider[] _colliders;
    private TransformMoveView _transformMoveView;
    private ActiveStateSwitcher _activeStateSwitcher;

    public IEnumerable<FigureView> FigureViews => _figureViews;

    public void Initialize()
    {
        if (_figureViews.Length == 0)
            throw new EmptyCollectionException();

        _transformMoveView = GetComponent<TransformMoveView>();
        _activeStateSwitcher = GetComponent<ActiveStateSwitcher>();

        _transformMoveView.Initialize();
        _activeStateSwitcher.Initialize();

        _colliders = new Collider[_figureViews.Length];

        for (int i = 0; i < _colliders.Length; i++)
            _colliders[i] = _figureViews[i].Collider;
    }

    public void Enable()
    {
        _activeStateSwitcher.SetActive(true);
    }

    public void Disable()
    {
        _transformMoveView.SetStartTransform();
        _activeStateSwitcher.SetActive(false);
    }

    public void Move(Vector3 targetPosition)
    {
        _transformMoveView.ChangePosition(targetPosition, _colliders, _moveSpeed);
    }
}
