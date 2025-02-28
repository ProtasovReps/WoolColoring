using Reflex.Core;
using UnityEngine;

public class RopeConnectorInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private RopePool _ropePool;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(_ropePool);
    }
}
