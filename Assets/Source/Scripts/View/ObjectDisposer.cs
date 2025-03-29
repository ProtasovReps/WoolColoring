using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisposer : MonoBehaviour
{
    private List<IDisposable> _disposables = new List<IDisposable>();

    private void OnDestroy()
    {
        for (int i = 0; i < _disposables.Count; i++)
            _disposables[i].Dispose();

        Debug.Log($"{_disposables.Count} objects disposed");
    }

    public void Add(IDisposable disposable)
        => _disposables.Add(disposable);
}
