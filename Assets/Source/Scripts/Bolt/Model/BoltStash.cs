using System;
using System.Collections.Generic;

public class BoltStash
{
    private List<BoltView> _bolts;

    public event Action<IEnumerable<BoltView>> BoltsAdded;

    public BoltStash() => _bolts = new List<BoltView>();

    public void Add(IEnumerable<BoltView> bolts)
    {
        _bolts.AddRange(bolts);
        BoltsAdded?.Invoke(bolts);
    }

    public IEnumerable<BoltView> Bolts => _bolts;
}
