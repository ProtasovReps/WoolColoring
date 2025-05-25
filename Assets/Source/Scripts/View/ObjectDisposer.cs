using System;
using System.Collections.Generic;
using UnityEngine;

namespace ViewExtensions
{
    public class ObjectDisposer : MonoBehaviour
    {
        private List<IDisposable> _disposables = new List<IDisposable>();

        private void OnDestroy()
        {
            for (int i = 0; i < _disposables.Count; i++)
                _disposables[i].Dispose();
        }

        public void Add(IDisposable disposable)
        {
            _disposables.Add(disposable);
        }
    }
}