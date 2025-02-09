using System.Collections.Generic;
using UnityEngine;

public class BoltContainer : MonoBehaviour
{
    [SerializeField] private BoltView[] _bolts;

    public IEnumerable<BoltView> Bolts => _bolts;

    private void OnEnable()
    {
        for (int i = 0; i < _bolts.Length; i++)
            _bolts[i].gameObject.SetActive(true);
    }
}