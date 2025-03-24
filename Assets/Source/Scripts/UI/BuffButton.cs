using UnityEngine;
using System;
using Reflex.Attributes;
using Cysharp.Threading.Tasks;

public class BuffButton : ButtonView
{
    [SerializeField] private ActivatableUI _buyBuffMenu;
    [SerializeField] private ActivatableTextField _ruleText;
    [SerializeField] private BuffCountCounter _counter;
    [SerializeField] private float _coolDownTime;

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
        await UniTask.WaitForSeconds(_coolDownTime);
        Activate();
    }
}