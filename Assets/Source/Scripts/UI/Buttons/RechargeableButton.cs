using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using System.Threading;
using TMPro;
using UnityEngine;

public class RechargeableButton : ButtonView
{
    private const int SecondsInMinute = 60;

    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private MonoBehaviour _activeButtonObject;

    private AdTimer _timer;
    private CancellationTokenSource _cancellationToken;

    [Inject]
    private void Inject(AdTimer adTimer)
    {
        _timer = adTimer;
        _timer.TimeElapsed += Activate;
    }

    private void Awake()
    {
        if (_timer.IsCounting)
            Deactivate();
        else
            Activate();
    }

    private void OnEnable()
    {
        _cancellationToken = new CancellationTokenSource();
        ShowRemainingTime().Forget();
    }

    private void OnDisable()
    {
        _cancellationToken?.Cancel();
    }

    private void OnDestroy()
    {
        _timer.TimeElapsed -= Activate;
    }

    public override void Activate()
    {
        OnDisable();
        _counterText.gameObject.SetActive(false);
        _activeButtonObject.gameObject.SetActive(true);
        base.Activate();
    }

    public override void Deactivate()
    {
        _counterText.gameObject.SetActive(true);
        _activeButtonObject.gameObject.SetActive(false);
        base.Deactivate();
    }

    protected override void OnButtonClick()
    {
        _timer.Reset();
        Deactivate();
        base.OnButtonClick();
    }

    private async UniTaskVoid ShowRemainingTime()
    {
        while(_cancellationToken.IsCancellationRequested == false)
        {
            int remainingTime = (int)(_timer.CooldownTime - _timer.ElapsedTime);
            int remainingMinutes = remainingTime / SecondsInMinute;
            int remainingSeconds = remainingTime % SecondsInMinute;

            _counterText.text = $"{remainingMinutes}:{remainingSeconds}";
            await UniTask.Yield();
        }
    }
}