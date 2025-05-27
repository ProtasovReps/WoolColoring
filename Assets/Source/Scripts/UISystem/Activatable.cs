using UnityEngine;

namespace UISystem
{
    public abstract class Activatable : MonoBehaviour
    {
        public abstract void Activate();

        public abstract void Deactivate();
    }
}