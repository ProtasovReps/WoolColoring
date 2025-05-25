using System.Collections.Generic;
using UnityEngine;

namespace ConnectingRope
{
    public class RopePool : MonoBehaviour
    {
        [SerializeField] private Rope _prefab;

        private Queue<Rope> _freeRopes;
        private List<Rope> _busyRopes;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            _freeRopes = new Queue<Rope>();
            _busyRopes = new List<Rope>();
        }

        private void OnDisable()
        {
            foreach (Rope rope in _busyRopes)
                rope.Disconected -= Release;
        }

        public Rope Get()
        {
            Rope rope;

            if (_freeRopes.Count == 0)
            {
                rope = Instantiate(_prefab);

                rope.transform.SetParent(_transform);
                _freeRopes.Enqueue(rope);
            }

            rope = _freeRopes.Dequeue();
            rope.Disconected += Release;

            _busyRopes.Add(rope);
            rope.gameObject.SetActive(true);
            return rope;
        }

        private void Release(Rope rope)
        {
            rope.gameObject.SetActive(false);
            rope.Disconected -= Release;

            _busyRopes.Remove(rope);
            _freeRopes.Enqueue(rope);
        }
    }
}