using UnityEngine;

public class PictureView : MonoBehaviour
{
    [SerializeField] private PictureTransformView _transformView;
    [SerializeField] private Transform _startPositionBound;
    [SerializeField] private Transform _upperBlock;
    [SerializeField] private float _moveSpeed;

    public Transform Transform => _transformView.Transform;

    private void Awake() => _transformView.Initialize();

    public void Move(Transform colorBlock, Vector3 targetBound)
    {
       _transformView.ChangePosition(colorBlock, targetBound, _moveSpeed);
    }

    public void ResetPosition()
    {
        _transformView.ChangePosition(_upperBlock, _startPositionBound.position, _moveSpeed);
    }
}