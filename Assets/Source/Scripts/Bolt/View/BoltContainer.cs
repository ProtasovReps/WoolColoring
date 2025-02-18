using System.Collections.Generic;
using UnityEngine;

public class BoltContainer : MonoBehaviour
{
    [SerializeField] private BoltView[] _bolts;

    public IEnumerable<BoltView> Bolts => _bolts;
}