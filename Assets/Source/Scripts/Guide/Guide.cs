using UnityEngine;
using YG;

public class Guide : MonoBehaviour
{
    [SerializeField] private ReplicConveyer _conveyer;
    [SerializeField] private GuideBoltClickReader _guideClickReader;
    [SerializeField] private float _startDelay;

    private void OnEnable()
    {
        _conveyer.AllReplicsPlayed += FinalizeGuide;
    }

    private void Start()
    {
        if(YG2.saves.IfGuidePassed)
        {
            gameObject.SetActive(false);
            return;
        }

        _conveyer.gameObject.SetActive(true);
        _guideClickReader.SetPause(true);
    }

    private void OnDisable()
    {
        _conveyer.AllReplicsPlayed -= FinalizeGuide;
    }

    private void FinalizeGuide()
    {
        YG2.MetricaSend(MetricParams.GuidePassed.ToString());
        YG2.saves.IfGuidePassed = true;
    }
}