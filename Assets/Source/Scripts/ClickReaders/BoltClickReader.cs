using Reflex.Attributes;
using UnityEngine;

public class BoltClickReader : ClickReader
{
    private StringDistributor _distributor;

    protected override void ValidateHit(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Bolt bolt) == false)
            return;

        if (bolt.IsAnimating)
            return;

        _distributor.Distribute(bolt);
        bolt.Unscrew();
    }

    [Inject]
    private void Inject(StringDistributor distributor)
    {
        _distributor = distributor;
    }
}