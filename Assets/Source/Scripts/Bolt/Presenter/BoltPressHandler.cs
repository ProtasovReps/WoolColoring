using System;
using UnityEngine;

public class BoltPressHandler
{
    private readonly StringDistributor _distributor;

    public BoltPressHandler(StringDistributor distributor)
    {
        if (distributor == null)
            throw new ArgumentNullException(nameof(distributor));

        _distributor = distributor;
    }

    public void ProcessClick(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Bolt bolt) == false)
            return;

        _distributor.Distribute(bolt);
        bolt.Unscrew();
    }
}
