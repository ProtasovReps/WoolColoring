using System.Collections.Generic;
using UnityEngine;

public class BoltStash : MonoBehaviour
{
    [SerializeField] private StringBolt[] _bolts;

    public IReadOnlyCollection<StringBolt> Bolts => _bolts;
}
