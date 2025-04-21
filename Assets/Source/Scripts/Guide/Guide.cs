using Reflex.Attributes;
using UnityEngine;

public class Guide : MonoBehaviour
{
    [SerializeField] private ReplicPlayer[] _replicPlayers;
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private GuideBoltClickReader _guideClickReader;

    private BoltClickReader _reader;
    private int _replicPlayerIndex;

    [Inject]
    private void Inject(BoltClickReader reader)
    {
        _reader = reader;
    }

    private void Awake()
    {
        _reader.SetPause(true);
        _guideClickReader.SetPause(true);
        _levelUI.gameObject.SetActive(false);
        ShowReplicPlayers();
    }

    private void ShowReplicPlayers()
    {
        if (_replicPlayerIndex == _replicPlayers.Length)
            return;

        ReplicPlayer player = _replicPlayers[_replicPlayerIndex];

        player.Activate();
        player.Executed += OnPlayerExecuted;
    }

    private void OnPlayerExecuted(ReplicPlayer replic)
    {
        replic.Executed -= OnPlayerExecuted;
        _replicPlayerIndex++;
        ShowReplicPlayers();
    }
}