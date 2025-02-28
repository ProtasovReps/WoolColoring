using Reflex.Core;
using UnityEngine;

public class BoltInstaller : MonoBehaviour, IInstaller
{
    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(typeof(BoltStash));
    }
}
