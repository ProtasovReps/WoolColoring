using UnityEngine;

namespace ViewExtensions
{
    public class ActiveStateSwitcher : MonoBehaviour
    {
        private Transform _transform;

        public void Initialize()
        {
            _transform = transform;
        }

        public void SetActive(bool isActive)
        {
            _transform.gameObject.SetActive(isActive);
        }
    }
}