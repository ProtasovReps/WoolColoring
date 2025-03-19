using UnityEngine;
using System;

public class BuffButton : ButtonView
{
    [SerializeField] private ActivatableUI _buyBuffMenu;

    private BuffBag _bag;
    private IBuff _strategy;

    public void Initialize(IBuff buff, BuffBag buffBag)
    {
        if (buff == null)
            throw new ArgumentNullException(nameof(buff));

        if (buffBag == null)
            throw new ArgumentNullException(nameof(buffBag));

        _strategy = buff;
        _bag = buffBag;
    }

    protected override void OnButtonClick()
    {
        base.OnButtonClick();

        if (_strategy.Validate() == false)
            return; // показывать текст "баф невозвожно использовать сейчас"

        if (_bag.TryGetBuff(_strategy) == false)
        {
            _buyBuffMenu.Activate();
            return;
        }

        _strategy.Execute();
    }
}