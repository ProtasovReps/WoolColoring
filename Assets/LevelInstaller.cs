using UnityEngine;
using Reflex.Core;

public class LevelInstaller : MonoBehaviour, IInstaller
{
    [Header("FigureConveyer")]
    [SerializeField] private ConveyerPosition[] _conveyerPositions;
    [SerializeField] private FigureCompositionFactory _figureCompositionFactory;
    [Header("RopeConnector")]
    [SerializeField] private RopePool _ropePool;
    [Header("HolderView")]
    [SerializeField] private ColoredStringHolderView[] _coloredViews;
    [SerializeField] private WhiteStringHolderView _whiteStringHolderView;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        InstallBolt(containerBuilder);
        InstallFigureComposition(containerBuilder);
        InstallHoldersViews(containerBuilder);
        InstallRopeConnector(containerBuilder);
    }

    private void InstallFigureComposition(ContainerBuilder containerBuilder)
    {
        var positionDatabase = new PositionDatabase(_conveyerPositions);
        var compositionPool = new FigureCompositionPool(_figureCompositionFactory);
        var conveyer = new Conveyer(compositionPool, positionDatabase);

        containerBuilder.AddSingleton(conveyer);
    }

    private void InstallPicture(ContainerBuilder containerBuilder)
    {

    }

    private void InstallBolt(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(typeof(BoltStash));
    }

    private void InstallHoldersViews(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(_coloredViews);
        containerBuilder.AddSingleton(_whiteStringHolderView);
    }

    private void InstallRopeConnector(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(_ropePool);
    }
}
