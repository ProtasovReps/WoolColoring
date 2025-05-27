using System.Collections.Generic;
using UnityEngine;
using ColorStringSystem.View;
using Extensions.View;

namespace StringHolderSystem.View
{
    [RequireComponent(typeof(TransformView))]
    public class StringHolderView : MonoBehaviour
    {
        [SerializeField] private ColorStringView[] _strings;

        private TransformView _transformView;
        private StringHolderAnimations _stringHolderAnimations;

        public IEnumerable<ColorStringView> Strings => _strings;
        public Transform Transform => _transformView.Transform;
        protected StringHolderAnimations Animations => _stringHolderAnimations;
        protected TransformView TransformView => _transformView;

        public virtual void Initialize(StringHolderAnimations holderAnimations)
        {
            _stringHolderAnimations = holderAnimations;
            _transformView = GetComponent<TransformView>();
            _transformView.Initialize();
        }

        public void Shake()
        {
            _stringHolderAnimations.Shake(_transformView.Transform, _transformView.StartScale);
        }

        public bool TryGetFreeStringTransform(out Transform transform)
        {
            for (int i = 0; i < _strings.Length; i++)
            {
                if (_strings[i].IsAnimating)
                {
                    break;
                }

                if (_strings[i].gameObject.activeSelf == false)
                {
                    transform = _strings[i].Transform;
                    return true;
                }
            }

            transform = null;
            return false;
        }
    }
}