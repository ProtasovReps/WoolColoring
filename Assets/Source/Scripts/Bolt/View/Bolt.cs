using LitMotion;
using LitMotion.Extensions;
using System;
using UnityEngine;

[RequireComponent(typeof(EffectPlayer))]
[RequireComponent(typeof(TransformMoveView))]
[RequireComponent(typeof(ActiveStateSwitcher))]
[RequireComponent(typeof(HingeJoint))]
public class Bolt : MonoBehaviour
{
    [SerializeField] private BoltColorString _colorString;
    [SerializeField] private float _unscrewDuration = 0.25f;
    [SerializeField] private int _unscrewLoopCount = 4;

    private HingeJoint _hingeJoint;
    private Rigidbody _connectedBody;
    private ActiveStateSwitcher _activeStateSwitcher;
    private TransformMoveView _moveView;

    public event Action<Bolt> Disabling;

    public Transform Transform => _moveView.Transform;
    public IColorSettable ColorSettable => _colorString;
    public IColorable Colorable => _colorString;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _activeStateSwitcher = GetComponent<ActiveStateSwitcher>();
        _moveView = GetComponent<TransformMoveView>();
        _connectedBody = _hingeJoint.connectedBody;

        _activeStateSwitcher.Initialize();
        _moveView.Initialize();
        _colorString.Initialize();
    }

    private void OnEnable()
    {
        _moveView.SetStartTransform();
        _hingeJoint.connectedBody = _connectedBody;
    }

    public void Unscrew()
    {
        Vector3 rotation = _moveView.Transform.localRotation.eulerAngles;
        float targetRotation = rotation.y - 360f;
        Vector3 targetPosition = new(_moveView.Transform.position.x, 4.3f, -6.5f);
        Vector3 targetScale = _moveView.Transform.localScale * 1.2f;

        _hingeJoint.connectedBody = null;
        _connectedBody.AddRelativeTorque(Vector3.one);

        LSequence.Create()
            .Join(LMotion.Create(rotation, new Vector3(rotation.x, targetRotation, rotation.z), _unscrewDuration)
                .WithLoops(_unscrewLoopCount, LoopType.Incremental)
                .WithOnComplete(Disable)
                .BindToLocalEulerAngles(_moveView.Transform))
            .Join(LMotion.Create(_moveView.Transform.position, targetPosition, _unscrewDuration)
                .WithEase(Ease.InOutQuint)
                .BindToPosition(_moveView.Transform))
            .Join(LMotion.Create(_moveView.Transform.localScale, targetScale, _unscrewDuration)
                .WithLoops(_unscrewLoopCount, LoopType.Yoyo)
                .BindToLocalScale(_moveView.Transform))
            .Run();
    }

    public void SetActive(bool isActive) => _activeStateSwitcher.SetActive(isActive);

    private void Disable()
    {
        _moveView.Transform.rotation = _moveView.StartRotation;

        Disabling?.Invoke(this);
        SetActive(false);
    }
}