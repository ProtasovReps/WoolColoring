using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CounterButton : ButtonView
{
    [SerializeField] private TMP_Text _counterText;
    [SerializeField] private MonoBehaviour _activeButtonObject;
    [SerializeField] private float _reloadTime;

    public void SetReload() => OnButtonClick();

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