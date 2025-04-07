using UnityEngine;

public class FigureClickReader : ClickReader
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private BoltClickReader _boltClickReader;

    public override void SetPause(bool isPaused)
    {
        _boltClickReader.SetPause(!isPaused);
        base.SetPause(isPaused);
    }

    protected override void ValidateHit(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out ExplodableFigure explodable) == false)
            return;

        _particleSystem.transform.position = hit.point;

        _particleSystem.Play();
        explodable.Explode();
        SetPause(true);
    }
}