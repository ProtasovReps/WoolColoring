using System.Collections.Generic;

public class BoltStash
{
    private List<BoltView> _bolts;

    public BoltStash() => _bolts = new List<BoltView>();

    public void Add(IEnumerable<BoltView> bolts) => _bolts.AddRange(bolts);

    public IEnumerable<BoltView> Bolts => _bolts;
}
