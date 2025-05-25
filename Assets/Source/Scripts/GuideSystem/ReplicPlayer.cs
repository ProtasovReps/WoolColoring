using System;
using UnityEngine;

namespace PlayerGuide
{
    public class ReplicPlayer : MonoBehaviour
    {
        [SerializeField] private Replic[] _replics;

        private int _replicPlayerIndex;

        public event Action<ReplicPlayer> Executed;

        public void ShowReplics()
        {
            transform.gameObject.SetActive(true);

            if (_replicPlayerIndex == _replics.Length)
            {
                Executed?.Invoke(this);
                transform.gameObject.SetActive(false);
                return;
            }

            Replic replic = _replics[_replicPlayerIndex];

            replic.Activate();
            replic.Executed += OnReplicExecuted;
        }

        private void OnReplicExecuted(Replic replic)
        {
            replic.Executed -= OnReplicExecuted;
            _replicPlayerIndex++;
            ShowReplics();
        }
    }
}