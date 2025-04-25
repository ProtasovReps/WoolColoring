using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

public class RechargeableButton : ButtonView
{
    private const int SecondsInMinute = 60;

    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private MonoBehaviour _activeButtonObject;

    private AdTimer _timer;

    [Inject]
    private void Inject(AdTimer adTimer)
    {
        _timer = adTimer;
        _timer.TimeElapsed += Activate;
        _timer.Reseted += Deactivate;
    }

    private void Awake()
    {
        if (_timer.IsCounting)
           Deactivate();
    }

    private void OnDestroy()
    {
        _timer.TimeElapsed -= Activate;
        _timer.Reseted -= Deactivate;
    }

    public override void Activate()
    {
        _counterText.gameObject.SetActive(false);
        _activeButtonObject.gameObject.SetActive(true);
        base.Activate();
    }

    public override void Deactivate()
    {
        _counterText.gameObject.SetActive(true);
        _activeButtonObject.gameObject.SetActive(false);
        ShowRemainingTime().Forget();
        base.Deactivate();
    }

    protected override void OnButtonClick()
    {
        _timer.Reset();
        base.OnButtonClick();
    }

    private async UniTaskVoid ShowRemainingTime()
    {
        while (_timer.ElapsedTime < _timer.CooldownTime)
        {
            int remainingTime = (int)(_timer.CooldownTime - _timer.ElapsedTime);
            int remainingMinutes = remainingTime / SecondsInMinute;
            int remainingSeconds = remainingTime % SecondsInMinute;

            _counterText.text = $"{remainingMinutes}:{remainingSeconds}";
            await UniTask.Yield();
        }
    }
}