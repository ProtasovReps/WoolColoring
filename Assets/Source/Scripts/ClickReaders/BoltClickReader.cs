using Reflex.Attributes;
using UnityEngine;

public class BoltClickReader : ClickReader
{
    private const int ImmortalityRequiredColorsCount = 2;

    private StringDistributor _distributor;
    private WhiteStringHolder _whiteStringHolder;
    private Picture _picture;

    [Inject]
    private void Inject(StringDistributor distributor, WhiteStringHolder whiteStringHolder, Picture picture)
    {
        _distributor = distributor;
        _whiteStringHolder = whiteStringHolder;
        _picture = picture;
    }

    protected override void ValidateHit(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out Bolt bolt) == false)
            return;

        if (bolt.IsAnimating)
            return;

        if(_picture.RequiredColorsCount <= ImmortalityRequiredColorsCount)
        {
            if(_whiteStringHolder.StringCount >= _whiteStringHolder.MaxStringCount)
            {
                bolt.Unscrew();
                return;
            }
        }

        _distributor.Distribute(bolt);
        bolt.Unscrew();
    }
}