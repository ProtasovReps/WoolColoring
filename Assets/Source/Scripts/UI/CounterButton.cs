using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CounterButton : ButtonView
{
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private TMP_Text _activeButtonText;
    [SerializeField] private float _reloadTime;

    private void Awake() => OnButtonClick();

    public override void Activate()
    {
        _counterText.enabled = false;
        _activeButtonText.enabled = true;
        base.Activate();
    }

    public override void Deactivate()
    {
        _counterText.enabled = true;
        _activeButtonText.enabled = false;
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

        while(elapsedTime < _reloadTime)
        {
            int remainingTime = (int)(_reloadTime - elapsedTime);
            int remainingMinutes = remainingTime / 60;
            int remainingSeconds = remainingTime % 60;

            SetCount(remainingMinutes, remainingSeconds);
            elapsedTime += Time.unscaledDeltaTime;
            await UniTask.Yield();
        }

        Activate();
    }

    private void SetCount(int minutes, int seconds)
       => _counterText.text = $"{minutes}:{seconds}";
}