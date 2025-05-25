using System;
using UnityEngine;
using UnityEngine.UI;
using Reflex.Attributes;
using ClickReaders;
using LevelInterface;

namespace PlayerGuide
{
    public class ReplicConveyer : MonoBehaviour
    {
        [SerializeField] private ReplicPlayer[] _replicPlayers;
        [SerializeField] private LevelUI _levelUI;
        [SerializeField] private Button[] _buttonsToDisable;

        private BoltClickReader _boltClickReader;
        private int _replicPlayerIndex;

        public event Action AllReplicsPlayed;

        [Inject]
        private void Inject(BoltClickReader reader)
        {
            _boltClickReader = reader;
        }

        private void Start()
        {
            SetButtonsInteractable(false);
            _boltClickReader.SetPause(true);
            _levelUI.gameObject.SetActive(false);
            ShowReplicPlayer();
        }

        private void SetButtonsInteractable(bool isInteractable)
        {
            for (int i = 0; i < _buttonsToDisable.Length; i++)
            {
                _buttonsToDisable[i].interactable = isInteractable;
            }
        }

        private void ShowReplicPlayer()
        {
            if (_replicPlayerIndex == _replicPlayers.Length)
            {
                FinalizeReplics();
                return;
            }

            ReplicPlayer player = _replicPlayers[_replicPlayerIndex];

            player.ShowReplics();
            player.Executed += OnPlayerExecuted;
        }

        private void OnPlayerExecuted(ReplicPlayer replic)
        {
            replic.Executed -= OnPlayerExecuted;
            _replicPlayerIndex++;
            ShowReplicPlayer();
        }

        private void FinalizeReplics()
        {
            _levelUI.gameObject.SetActive(true);
            SetButtonsInteractable(true);
            _boltClickReader.SetPause(false);

            AllReplicsPlayed?.Invoke();
        }
    }
}