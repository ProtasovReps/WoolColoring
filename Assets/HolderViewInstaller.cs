using Reflex.Core;
using UnityEngine;

public class HolderViewInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private ColoredStringHolderView[] _coloredViews;
    [SerializeField] private WhiteStringHolderView _whiteStringHolderView;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(_coloredViews);
        containerBuilder.AddSingleton(_whiteStringHolderView);
    }
}
