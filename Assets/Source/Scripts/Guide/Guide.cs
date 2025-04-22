using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class Guide : MonoBehaviour
{
    [SerializeField] private ReplicPlayer[] _replicPlayers;
    [SerializeField] private LevelUI _levelUI;
    [SerializeField] private Button[] _levelUIButtons;
    [SerializeField] private GuideBoltClickReader _guideClickReader;
    [SerializeField] private float _startDelay;

    private BoltClickReader _boltClickReader;
    private int _replicPlayerIndex;

    [Inject]
    private void Inject(BoltClickReader reader)
    {
        _boltClickReader = reader;
    }

    private void Start()
    {
        if(YG2.saves.IfGuidePassed)
        {
            gameObject.SetActive(false);
            return;
        }

        SetButtonsInteractable(false);
        _boltClickReader.SetPause(true);
        _guideClickReader.SetPause(true);
        _levelUI.gameObject.SetActive(false);
        ShowReplicPlayers();
    }

    private void ShowReplicPlayers()
    {
        if (_replicPlayerIndex == _replicPlayers.Length)
        {
            FinalizeGuide();
            return;
        }

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

    private void FinalizeGuide()
    {
        SetButtonsInteractable(true);
        _boltClickReader.SetPause(false);
        YG2.MetricaSend(MetricParams.GuidePassed.ToString());
        YG2.saves.IfGuidePassed = true;
        YG2.SaveProgress();
    }

    private void SetButtonsInteractable(bool isInteractable)
    {
        for (int i = 0; i < _levelUIButtons.Length; i++)
        {
            _levelUIButtons[i].interactable = isInteractable;
        }
    }
}