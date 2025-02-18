using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

[RequireComponent(typeof(EffectsPlayer))]
[RequireComponent(typeof(TransformMoveView))]
[RequireComponent(typeof(ActiveStateSwitcher))]
[RequireComponent(typeof(HingeJoint))]
public class BoltView : MonoBehaviour
{
    [SerializeField] private ColorString _colorString;

    private EffectsPlayer _effectPlayer;
    private HingeJoint _hingeJoint;
    private Transform _transform;
    private Rigidbody _connectedBody;
    private ActiveStateSwitcher _activeStateSwitcher;
    private TransformMoveView _moveView;

    public IColorSettable ColorSettable => _colorString;
    public IColorable Colorable => _colorString;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _activeStateSwitcher = GetComponent<ActiveStateSwitcher>();
        _effectPlayer = GetComponent<EffectsPlayer>();
        _moveView = GetComponent<TransformMoveView>();
        _transform = transform;
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
        Vector3 rotation = _transform.localRotation.eulerAngles;
        float targetRotation = rotation.y + 360f;
        Vector3 targetPosition = new(_transform.position.x, 4.3f, -6.5f);
        Vector3 targetScale = _transform.localScale * 1.2f;
        float duration = 0.25f;
        int loopCount = 4;

        _hingeJoint.connectedBody = null;
        _connectedBody.AddRelativeTorque(Vector3.one);

        LSequence.Create()
            .Join(LMotion.Create(rotation, new Vector3(rotation.x, targetRotation, rotation.z), duration)
                .WithLoops(loopCount, LoopType.Incremental)
                .WithOnComplete(Disable)
                .BindToLocalEulerAngles(_transform))
            .Join(LMotion.Create(_transform.position, targetPosition, duration)
                .WithEase(Ease.InOutQuint)
                .BindToPosition(_transform))
            .Join(LMotion.Create(_transform.localScale, targetScale, duration)
                .WithLoops(loopCount, LoopType.Yoyo)
                .BindToLocalScale(_transform))
            .Run();
    }

    public void SetActive(bool isActive) => _activeStateSwitcher.SetActive(isActive);

    private void Disable()
    {
        _effectPlayer.Play();

        SetActive(false);
    }
}