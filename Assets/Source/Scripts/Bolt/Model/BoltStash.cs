using System.Collections.Generic;

public class BoltStash
{
    private List<BoltView> _bolts;

    public BoltStash() => _bolts = new List<BoltView>();

    public void Add(IReadOnlyCollection<BoltView> bolts) => _bolts.AddRange(bolts);

    public IReadOnlyCollection<BoltView> Bolts => _bolts;
}
