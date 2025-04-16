using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;

public class RechargeableButton : ButtonView
{
    private const int SecondsInMinute = 60;

    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private MonoBehaviour _activeButtonObject;
    [SerializeField] private float _reloadTime;

    public event Action Recharged;

    public void SetReload() => Reload().Forget();

    public override void Activate()
    {
        Recharged?.Invoke();
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
        Reload().Forget();
        base.OnButtonClick();
    }

    private async UniTaskVoid Reload()
    {
        float elapsedTime = 0f;

        Deactivate();

        while (elapsedTime < _reloadTime)
        {
            int remainingTime = (int)(_reloadTime - elapsedTime);
            int remainingMinutes = remainingTime / SecondsInMinute;
            int remainingSeconds = remainingTime % SecondsInMinute;

            SetCount(remainingMinutes, remainingSeconds);
            elapsedTime += Time.unscaledDeltaTime;
            await UniTask.Yield();
        }

        Activate();
    }

    private void SetCount(int minutes, int seconds)
       => _counterText.text = $"{minutes}:{seconds}";
}