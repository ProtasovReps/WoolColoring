using System;
using System.Collections.Generic;
using Bolts.View;

namespace Bolts
{
    public class BoltStash
    {
        private readonly List<Bolt> _bolts;

        public BoltStash()
        {
            _bolts = new List<Bolt>();
        }

        public event Action<IEnumerable<Bolt>> BoltsAdded;

        public IEnumerable<Bolt> Bolts => _bolts;

        public void Add(IEnumerable<Bolt> bolts)
        {
            _bolts.AddRange(bolts);
            BoltsAdded?.Invoke(bolts);
        }
    }
}