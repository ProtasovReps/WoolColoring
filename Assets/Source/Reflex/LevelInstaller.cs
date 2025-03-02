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
    [SerializeField] private int _startHoldersCount;
    [Header("Picture")]
    [SerializeField] private ColorBlockViewStash _colorBlockViewStash;
    [SerializeField] private PictureView _pictureView;
    [SerializeField] private Malbert _malbert;
    [Header("Bolt")]
    [SerializeField] private BoltClickReader _boltClickReader;

    private BoltColorSetter _boltColorSetter;
    private Conveyer _conveyer;

    private void Start()
    {
        _conveyer.FillAllFigures();
        _boltColorSetter.SetColors();
    }

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        InstallFigureComposition(containerBuilder);

        Picture picture = InstallPicture(containerBuilder);
        StringDistributor stringDistributor = InstallHolders(containerBuilder, picture);

        InstallBolt(containerBuilder, stringDistributor, picture);
        InstallRopeConnector(containerBuilder);
    }

    private void InstallFigureComposition(ContainerBuilder containerBuilder)
    {
        _figureCompositionFactory.Initailize();

        var positionDatabase = new PositionDatabase(_conveyerPositions);
        var compositionPool = new FigureCompositionPool(_figureCompositionFactory);
        _conveyer = new Conveyer(compositionPool, positionDatabase);
    }

    private Picture InstallPicture(ContainerBuilder containerBuilder)
    {
        _colorBlockViewStash.Initialize();

        ColorBlockView[] blockViews = _colorBlockViewStash.GetBlockViews();
        ColorBlockBinder colorBlockBinder = new(blockViews, _blockHolderConnector);
        PictureBinder pictureBinder = new(_pictureView, colorBlockBinder, _malbert);
        Picture picture = pictureBinder.Bind();

        containerBuilder.AddSingleton(picture);
        return picture;
    }

    private void InstallBolt(ContainerBuilder containerBuilder, StringDistributor stringDistributor, Picture picture)
    {
        var boltStash = new BoltStash();
        var boltPressHandler = new BoltPressHandler(stringDistributor);
        _boltColorSetter = new BoltColorSetter(boltStash, picture);

        _boltClickReader.Initialize(boltPressHandler);
        containerBuilder.AddSingleton(boltStash);
    }

    private StringDistributor InstallHolders(ContainerBuilder containerBuilder, Picture picture)
    {
        ColorStringFactory colorStringFactory = new();
        StringHolderBinder stringHolderBinder = new(colorStringFactory, _coloredViews, _whiteStringHolderView);
        ColoredStringHolder[] coloredHolderModels = stringHolderBinder.BindColoredHolders();
        WhiteStringHolder whiteHolderModel = stringHolderBinder.BindWhiteHolder();
        ColoredStringHolderStash coloredStringHolderStash = new(coloredHolderModels, _startHoldersCount);
        ColoredStringHolderSwitcher switcher = new(picture, coloredStringHolderStash);
        StringDistributor stringDistributor = new(coloredStringHolderStash, whiteHolderModel, switcher);
        ExtraStringRemover stringRemover = new(picture, whiteHolderModel);

        containerBuilder.AddSingleton(coloredStringHolderStash);
        containerBuilder.AddSingleton(switcher);
        containerBuilder.AddSingleton(stringDistributor);
        containerBuilder.AddSingleton(_coloredViews);
        containerBuilder.AddSingleton(_whiteStringHolderView);

        foreach (var holder in coloredStringHolderStash.ColoredStringHolders)
            switcher.ChangeStringHolderColor(holder as ColoredStringHolder);

        return stringDistributor;
    }

    private void InstallRopeConnector(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(_ropePool);
    }
}
