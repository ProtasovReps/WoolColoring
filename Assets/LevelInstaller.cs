using UnityEngine;
using Reflex.Core;

public class LevelInstaller : MonoBehaviour, IInstaller
{
    [Header("FigureConveyer")]
    [SerializeField] private ConveyerPosition[] _conveyerPositions;
    [SerializeField] private FigureCompositionFactory _figureCompositionFactory;
    [Header("RopeConnector")]
    [SerializeField] private RopePool _ropePool;
    [SerializeField] private BlockHolderConnector _blockHolderConnector;
    [Header("HolderView")]
    [SerializeField] private ColoredStringHolderView[] _coloredViews;
    [SerializeField] private WhiteStringHolderView _whiteStringHolderView;
    [Header("Picture")]
    [SerializeField] private ColorBlockViewStash _colorBlockViewStash;
    [SerializeField] private PictureView _pictureView;
    [SerializeField] private Malbert _malbert;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        InstallFigureComposition(containerBuilder);
        InstallBolt(containerBuilder);
        InstallPicture(containerBuilder);
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
        _colorBlockViewStash.Initialize();

        var colorBlockBinder = new ColorBlockBinder(_colorBlockViewStash.GetBlockViews(), _blockHolderConnector);
        var pictureBinder = new PictureBinder();
        ColorBlock[] colorBlocks = colorBlockBinder.Bind();
        Picture picture = pictureBinder.Bind(_pictureView, colorBlocks, _malbert);

        containerBuilder.AddSingleton(picture, typeof(Picture));
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
