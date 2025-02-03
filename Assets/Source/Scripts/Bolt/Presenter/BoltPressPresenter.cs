using System;
using UnityEngine;

public class BoltPressPresenter
{
    private readonly PlayerClickView _inputReader;
    private readonly StringDistributor _distributor;

    public BoltPressPresenter(PlayerClickView inputReader, StringDistributor distributor)
    {
        if (inputReader == null)
            throw new ArgumentNullException(nameof(inputReader));

        if (distributor == null)
            throw new ArgumentNullException(nameof(distributor));

        _inputReader = inputReader;
        _distributor = distributor;
    }

    public void ProcessClick(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out StringBolt bolt) == false)
            return;

        _distributor.Distribute(bolt);
    }
}
