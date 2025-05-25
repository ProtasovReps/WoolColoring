using System;
using TMPro;
using UnityEngine;

namespace PlayerGuide
{
    public abstract class Replic : MonoBehaviour
    {
        [SerializeField] private TMP_Text _replic;
        [SerializeField] private ReplicAnimation _replicAnimation;
        [SerializeField] private Transform _transform;

        public event Action<Replic> Executed;

        public virtual void Activate()
        {
            _transform.gameObject.SetActive(true);
            _replicAnimation.Activate(_replic, OnAnimationFinalized);
        }

        protected abstract void OnAnimationFinalized();

        protected virtual void Deactivate()
        {
            _transform.gameObject.SetActive(false);
            Executed?.Invoke(this);
        }
    }
}