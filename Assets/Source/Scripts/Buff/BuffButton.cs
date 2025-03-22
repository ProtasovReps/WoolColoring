using UnityEngine;
using System;
using Reflex.Attributes;

public class BuffButton : ButtonView
{
    [SerializeField] private ActivatableUI _buyBuffMenu;
    [SerializeField] private BuffCountCounter _counter;

    private BuffBag _bag;
    private IBuff _buff;

    public void Initialize(IBuff buff)
    {
        if (buff == null)
            throw new ArgumentNullException(nameof(buff));

        _buff = buff;
    }

    protected override void OnButtonClick()
    {
        base.OnButtonClick();

        if (_buff.Validate() == false)
        {
            Debug.Log("едрена вош");
            return; // показывать текст "баф невозвожно использовать сейчас"
        }

        if (_bag.TryGetBuff(_buff) == false)
        {
            _buyBuffMenu.Activate();
            return;
        }

        _buff.Execute();
    }

    [Inject]
    private void Inject(BuffBag buffBag)
    {
        _bag = buffBag;
        _counter.Initialize(_buff);
    }
}