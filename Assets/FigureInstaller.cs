using Reflex.Core;
using UnityEngine;

public class FigureInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private ConveyerPosition[] _conveyerPositions;
    [SerializeField] private FigureFactory _figureFactory;
    [SerializeField] private FigureCompositionFactory _figureCompositionFactory;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(_figureFactory);
        containerBuilder.AddSingleton(_conveyerPositions);
        containerBuilder.AddSingleton(typeof(FigureCompositionPool));
        containerBuilder.AddSingleton(typeof(PositionDatabase));
        containerBuilder.AddSingleton(_figureCompositionFactory);
        containerBuilder.AddSingleton(typeof(Conveyer));
    }
}