using System.Collections.Generic;
using UnityEngine;

namespace BoltSystem.View
{
    public class BoltContainer : MonoBehaviour
    {
        [SerializeField] private Bolt[] _bolts;

        public IEnumerable<Bolt> Bolts => _bolts;
    }
}