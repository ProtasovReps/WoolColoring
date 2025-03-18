using Reflex.Attributes;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuffButton : ActivatableButton
{
    [SerializeField] private Button _button;

    private Wallet _wallet;
    private IBuffStrategy _strategy;

    private void OnEnable()
    {
        _button.onClick.AddListener(ExecuteStrategy);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(ExecuteStrategy);
    }

    public void Initialize(IBuffStrategy strategy)
    {
        if(strategy == null)
            throw new ArgumentNullException(nameof(strategy));

        _strategy = strategy;
    }

    [Inject]
    private void Inject(Wallet wallet)
    {
        _wallet = wallet;
    }

    private void ExecuteStrategy()
    {
        if (_strategy.Validate() == false)
            return;

        if (_wallet.TrySpend(_strategy.Price) == false)
            return;

        _strategy.Execute();
    }
}