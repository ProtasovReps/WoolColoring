using UnityEngine;

namespace StringHolders.View
{
    public class WhiteStringHolderView : StringHolderView
    {
        [SerializeField] private float _appearDuration;

        private void Awake()
        {
            Appear();
        }

        private void Appear()
        {
            Animations.Appear(Transform);
        }
    }
}