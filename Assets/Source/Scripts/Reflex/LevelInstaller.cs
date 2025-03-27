using UnityEngine;
using Reflex.Core;
using Ami.BroAudio;
using System.Collections.Generic;

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
    [SerializeField] private StringHolderAnimations _holderAnimations;
    [SerializeField] private HolderSoundPlayer _holderSoundPlayer;
    [SerializeField, Range(1, 4)] private int _startHoldersCount;
    [Header("Picture")]
    [SerializeField] private ColorBlockViewStash _colorBlockViewStash;
    [SerializeField] private ColorBlockAnimations _colorBlockAnimations;
    [SerializeField] private BlockSoundPlayer _blockSoundPlayer;
    [SerializeField] private PictureView _pictureView;
    [SerializeField] private Malbert _malbert;
    [SerializeField] private Painter _painter;
    [Header("ClickReaders")]
    [SerializeField] private BoltClickReader _boltClickReader;
    [SerializeField] private FigureClickReader _figureClickReader;
    [Header("LevelMusic")]
    [SerializeField] private SoundID _soundID;
    [Header("UI")]
    [SerializeField] private ActivatableUIInitializator _activatableInitializator;
    [SerializeField] private BuffInitializer _buffInitializer;
    [SerializeField] private UIAnimator _uIAnimator;

    private BoltColorSetter _boltColorSetter;
    private Conveyer _conveyer;
    private UnlockHolderStrategy _unlockHolderStrategy;
    private FillHolderStrategy _fillHolderStrategy;
    private ClearWhiteHolderStrategy _clearStrategy;
    private PlayerInput _playerInput;

    private void Start()
    {
        _conveyer.FillAllFigures();
        _boltColorSetter.SetColors();
        _activatableInitializator.Initialize();
        BroAudio.Play(_soundID);
    }

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        _playerInput = new PlayerInput();

        InstallFigureComposition();

        Picture picture = InstallPicture(containerBuilder);
        StringDistributor stringDistributor = InstallHolders(containerBuilder, picture);

        InstallBolt(containerBuilder, stringDistributor, picture);
        InstallWallet(containerBuilder, picture);
        InstallBuffs(containerBuilder);
        InstallRopeConnector(containerBuilder);
        InstallClickReaders(containerBuilder);

        containerBuilder.AddSingleton(_uIAnimator);
    }

    private void InstallFigureComposition()
    {
        var positionDatabase = new PositionDatabase(_conveyerPositions);
        var compositionPool = new FigureCompositionPool(_figureCompositionFactory);
        _conveyer = new Conveyer(compositionPool, positionDatabase);

        _figureClickReader.Initialize(_playerInput);
    }

    private Picture InstallPicture(ContainerBuilder containerBuilder)
    {
        _colorBlockViewStash.Initialize();

        ColorBlockView[] blockViews = _colorBlockViewStash.GetBlockViews();
        ColorBlockBinder colorBlockBinder = new(blockViews, _blockHolderConnector);
        PictureBinder pictureBinder = new(_pictureView, colorBlockBinder, _malbert);
        Picture picture = pictureBinder.Bind();

        containerBuilder.AddSingleton(_blockSoundPlayer);
        containerBuilder.AddSingleton(_colorBlockAnimations);
        containerBuilder.AddSingleton(picture);
        return picture;
    }

    private void InstallBolt(ContainerBuilder containerBuilder, StringDistributor stringDistributor, Picture picture)
    {
        var boltStash = new BoltStash();
        _boltColorSetter = new BoltColorSetter(boltStash, picture);

        _boltClickReader.Initialize(stringDistributor, _playerInput);
        containerBuilder.AddSingleton(boltStash);
    }

    private StringDistributor InstallHolders(ContainerBuilder containerBuilder, Picture picture)
    {
        ColorStringFactory colorStringFactory = new();
        StringHolderBinder stringHolderBinder = new(colorStringFactory, _coloredViews, _whiteStringHolderView);
        ColoredStringHolder[] coloredHolderModels = stringHolderBinder.BindColoredHolders(_holderAnimations, _holderSoundPlayer);
        WhiteStringHolder whiteHolderModel = stringHolderBinder.BindWhiteHolder(_holderAnimations);
        ColoredStringHolderStash coloredStringHolderStash = new(coloredHolderModels, _startHoldersCount);
        ColoredStringHolderSwitcher switcher = new(picture, coloredStringHolderStash);
        StringDistributor stringDistributor = new(coloredStringHolderStash, whiteHolderModel, switcher);
        ExtraStringRemover stringRemover = new(picture, whiteHolderModel);

        _unlockHolderStrategy = new(coloredStringHolderStash, switcher, picture);
        _fillHolderStrategy = new(coloredHolderModels);
        _clearStrategy = new(whiteHolderModel);

        _painter.Initialize(coloredStringHolderStash, switcher);
        containerBuilder.AddSingleton(stringDistributor);
        containerBuilder.AddSingleton(_coloredViews);
        containerBuilder.AddSingleton(_whiteStringHolderView);

        foreach (var holder in coloredStringHolderStash.ActiveHolders)
            switcher.Switch(holder as ColoredStringHolder);

        return stringDistributor;
    }

    private void InstallRopeConnector(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(_ropePool);
    }

    private void InstallWallet(ContainerBuilder containerBuilder, Picture picture)
    {
        var wallet = new Wallet();
        var moneyRewards = new MoneyRewards(picture, wallet, _figureCompositionFactory);

        containerBuilder.AddSingleton(wallet, typeof(ICountChangeable), typeof(Wallet));
    }

    private void InstallBuffs(ContainerBuilder containerBuilder)
    {
        var explodeStrategy = new ExplodeFigureStrategy(_figureClickReader);

        Dictionary<IBuff, int> buffs = new()
        {
            { _unlockHolderStrategy, 1 },
            { _fillHolderStrategy, 5 },
            { _clearStrategy, 5 },
            { explodeStrategy, 5 },
        };

        BuffBag buffBag = new(buffs);

        containerBuilder.AddSingleton(buffBag);
        _buffInitializer.Initialize(_unlockHolderStrategy, _fillHolderStrategy, explodeStrategy, _clearStrategy);
    }

    private void InstallClickReaders(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(_boltClickReader);
        containerBuilder.AddSingleton(_figureClickReader);

        _figureClickReader.SetPause(true);
    }
}