using Ami.BroAudio;
using System;
using UnityEngine;

[RequireComponent(typeof(BoltAnimations))]
[RequireComponent(typeof(TransformView))]
[RequireComponent(typeof(ActiveStateSwitcher))]
[RequireComponent(typeof(HingeJoint))]
public class Bolt : MonoBehaviour
{
    [SerializeField] private BoltColorString _colorString;
    [SerializeField] private SoundID _unscrewSound;

    private HingeJoint _hingeJoint;
    private Rigidbody _connectedBody;
    private ActiveStateSwitcher _activeStateSwitcher;
    private TransformView _transformView;
    private BoltAnimations _animations;

    public event Action<Bolt> Disabling;

    public bool IsAnimating { get; private set; }
    public Transform Transform => _transformView.Transform;
    public IColorSettable ColorSettable => _colorString;
    public IColorable Colorable => _colorString;

    private void Awake()
    {
        _hingeJoint = GetComponent<HingeJoint>();
        _activeStateSwitcher = GetComponent<ActiveStateSwitcher>();
        _transformView = GetComponent<TransformView>();
        _animations = GetComponent<BoltAnimations>();
        _connectedBody = _hingeJoint.connectedBody;

        _activeStateSwitcher.Initialize();
        _transformView.Initialize();
        _colorString.Initialize();
    }

    private void OnEnable()
    {
        _transformView.SetStartTransform();
        _hingeJoint.connectedBody = _connectedBody;
    }

    public void Unscrew()
    {
        IsAnimating = true;
        _hingeJoint.connectedBody = null;

        _connectedBody.AddRelativeTorque(Vector3.one);
        _animations.Unscrew(_transformView.Transform, Disable);
        BroAudio.Play(_unscrewSound);
    }

    public void SetActive(bool isActive) => _activeStateSwitcher.SetActive(isActive);

    private void Disable()
    {
        _transformView.Transform.rotation = _transformView.StartRotation;

        Disabling?.Invoke(this);
        SetActive(false);

        IsAnimating = false;
    }
}