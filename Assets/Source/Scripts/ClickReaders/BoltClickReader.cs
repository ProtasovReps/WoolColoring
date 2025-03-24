using System;
using UnityEngine;

public class BoltClickReader : ClickReader
{
    private StringDistributor _distributor;

    public void Initialize(StringDistributor distributor, PlayerInput playerInput)
    {
        if (distributor == null)
            throw new ArgumentNullException(nameof(distributor));

        if(playerInput == null)
            throw new ArgumentNullException(nameof(playerInput));

        _distributor = distributor;
        Initialize(playerInput);
    }

    protected override void ValidateHit(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Bolt bolt) == false)
            return;

        if (bolt.IsAnimating)
            return;

        _distributor.Distribute(bolt);
        bolt.Unscrew();
    }
}