using UnityEngine;

namespace LevelInterface.Initializers
{
    public class ActivatableUIInitializator : MonoBehaviour
    {
        [SerializeField] private ActivatableUI[] _activatables;

        public void Initialize()
        {
            for (int i = 0; i < _activatables.Length; i++)
                _activatables[i].Initialize();
        }
    }
}
