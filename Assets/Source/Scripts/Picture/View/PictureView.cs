using System.Collections.Generic;
using UnityEngine;

public class PictureView : MonoBehaviour
{
    [SerializeField] private ColorBlockViewStash _colorBlocks;
    [SerializeField] private TransformMoveView _transformView;
    [SerializeField] private float _moveSpeed;

    public IEnumerable<ColorBlockView> ColorBlocks
        => _colorBlocks.ColorBlockViews;

    private void Awake() => _transformView.Initialize();

    public void Move(Transform colorBlock, Vector3 targetBound)
        => _transformView.ChangePosition(colorBlock, targetBound, _moveSpeed);
}