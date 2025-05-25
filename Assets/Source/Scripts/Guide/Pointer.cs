using Cysharp.Threading.Tasks;
using UnityEngine;

namespace PlayerGuide
{
    public class Pointer : MonoBehaviour
    {
        [SerializeField] private float _upPosition = 0.5f;
        [SerializeField] private PointerAnimation _pointerAnimation;

        private Transform _transform;
        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
            _transform = transform;
            _pointerAnimation.PopUp(_transform);

            Point().Forget();
        }

        private async UniTaskVoid Point()
        {
            while (_target.gameObject.activeSelf)
            {
                _transform.position = _target.position + Vector3.up * _upPosition;
                await UniTask.Yield();
            }

            _transform.gameObject.SetActive(false);
        }
    }
}