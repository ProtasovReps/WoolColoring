using System;
using System.Collections.Generic;

public class BoltStash
{
    private List<Bolt> _bolts;

    public event Action<IEnumerable<Bolt>> BoltsAdded;

    public BoltStash() => _bolts = new List<Bolt>();

    public void Add(IEnumerable<Bolt> bolts)
    {
        _bolts.AddRange(bolts);
        BoltsAdded?.Invoke(bolts);
    }

    public IEnumerable<Bolt> Bolts => _bolts;
}
