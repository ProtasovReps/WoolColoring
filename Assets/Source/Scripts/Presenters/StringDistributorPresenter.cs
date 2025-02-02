using System;
using UnityEngine;

public class StringDistributorPresenter : ISubscribable
{
    private InputReader _inputReader;
    private StringDistributor _distributor;

    public StringDistributorPresenter(InputReader inputReader, StringDistributor distributor)
    {
        if (inputReader == null)
            throw new ArgumentNullException(nameof(inputReader));

        if (distributor == null)
            throw new ArgumentNullException(nameof(distributor));

        _inputReader = inputReader;
        _distributor = distributor;
    }

    public void Subscribe()
    {
        _inputReader.Clicked += OnClicked;
    }

    public void Unsubscribe()
    {
        _inputReader.Clicked -= OnClicked;
    }

    private void OnClicked(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out StringBolt bolt))
        {
            _distributor.Distribute(bolt);
        }
    }
}
