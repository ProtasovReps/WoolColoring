using UnityEngine;
using System;
using Reflex.Attributes;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;

public class BuffButton : ButtonView
{
    [SerializeField] private BuffDealMenu _buyBuffMenu;
    [SerializeField] private TemporaryActivatableUI _ruleText;
    [SerializeField] private BuffCountCounter _counter;
    [SerializeField] private float _coolDownTime;
    [SerializeField] private Image _cooldownImage;

    private BuffBag _bag;
    private IBuff _buff;

    public void Initialize(IBuff buff)
    {
        if (buff == null)
            throw new ArgumentNullException(nameof(buff));

        _ruleText.Initialize();
        _buff = buff;
    }

    protected override void OnButtonClick()
    {
        base.OnButtonClick();

        if (_buff.Validate() == false)
        {
            _ruleText.Activate();
            return;
        }

        if (_bag.TryGetBuff(_buff) == false)
        {
            _buyBuffMenu.Activate();
            return;
        }

        WaitCoolDown().Forget();
        _buff.Execute();
    }

    [Inject]
    private void Inject(BuffBag buffBag)
    {
        _bag = buffBag;
        _counter.Initialize(_buff);
    }

    private async UniTaskVoid WaitCoolDown()
    {
        Deactivate();

        float elapsedTime = 0f;

        while (elapsedTime < _coolDownTime)
        {
            _cooldownImage.fillAmount = 1 - (elapsedTime / _coolDownTime);

            elapsedTime += Time.deltaTime;
            await UniTask.Yield();
        }

        _cooldownImage.fillAmount = 0f;
        Activate();
    }
}