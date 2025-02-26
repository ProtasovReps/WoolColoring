using System.Collections.Generic;
using UnityEngine;

public class BoltContainer : MonoBehaviour
{
    [SerializeField] private Bolt[] _bolts;

    public IEnumerable<Bolt> Bolts => _bolts;
}