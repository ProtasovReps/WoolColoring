using UnityEngine;

public class PictureView : MonoBehaviour
{
    [SerializeField] private PictureTransformView _transformView;
    [SerializeField] private float _moveSpeed;

    private void Awake() => _transformView.Initialize();

    public void Move(Transform colorBlock, Vector3 targetBound)
    {
       _transformView.ChangePosition(colorBlock, targetBound, _moveSpeed);
    }
}