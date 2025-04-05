using UnityEngine;
using Reflex.Core;
using Ami.BroAudio;
using YG;
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
    [Header("Disposer")]
    [SerializeField] private ObjectDisposer _disposer;

    private BoltColorSetter _boltColorSetter;
    private Conveyer _conveyer;
    private Unlocker _unlockHolderStrategy;
    private Filler _fillHolderStrategy;
    private Remover _removeStrategy;
    private PlayerInput _playerInput;

    private void Start()
    {
        _conveyer.FillAllFigures();
        _boltColorSetter.SetColors();
        _activatableInitializator.Initialize();
        BroAudio.Play(_soundID);
    }

    private void OnDestroy() => YG2.SaveProgress();

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        _playerInput = new PlayerInput();
        _disposer.Add(_playerInput);

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

        _figureCompositionFactory.Initialize(_disposer);
        _disposer.Add(_conveyer);
        _figureClickReader.Initialize(_playerInput);
    }

    private Picture InstallPicture(ContainerBuilder containerBuilder)
    {
        _colorBlockViewStash.Initialize();

        ColorBlockView[] blockViews = _colorBlockViewStash.GetBlockViews();
        ColorBlockBinder colorBlockBinder = new(blockViews, _blockHolderConnector, _disposer);
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

        _disposer.Add(_boltColorSetter);
        _boltClickReader.Initialize(stringDistributor, _playerInput);
        containerBuilder.AddSingleton(boltStash);
    }

    private StringDistributor InstallHolders(ContainerBuilder containerBuilder, Picture picture)
    {
        ColorStringFactory colorStringFactory = new();
        StringHolderBinder stringHolderBinder = new(colorStringFactory, _coloredViews, _whiteStringHolderView, _disposer);
        ColoredStringHolder[] coloredHolderModels = stringHolderBinder.BindColoredHolders(_holderAnimations, _holderSoundPlayer);
        WhiteStringHolder whiteHolderModel = stringHolderBinder.BindWhiteHolder(_holderAnimations);
        ColoredStringHolderStash coloredStringHolderStash = new(coloredHolderModels, _startHoldersCount);
        ColoredStringHolderSwitcher switcher = new(picture, coloredStringHolderStash);
        StringDistributor stringDistributor = new(coloredStringHolderStash, whiteHolderModel, switcher);
        ExtraStringRemover stringRemover = new(picture, whiteHolderModel);

        _unlockHolderStrategy = new(coloredStringHolderStash, switcher, picture);
        _fillHolderStrategy = new(coloredHolderModels);
        _removeStrategy = new(whiteHolderModel);

        _disposer.Add(stringRemover);
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
        var wallet = new Wallet(YG2.saves.Coins);
        var moneyRewards = new MoneyRewards(picture, wallet, _figureCompositionFactory);

        _disposer.Add(moneyRewards);
        containerBuilder.AddSingleton(wallet, typeof(ICountChangeable), typeof(Wallet));
    }

    private void InstallBuffs(ContainerBuilder containerBuilder)
    {
        var breakStrategy = new Breaker(_figureClickReader);
        var buffs = new Dictionary<IBuff, int>()
        {
                { _unlockHolderStrategy, YG2.saves.Unlockers },
                { _fillHolderStrategy, YG2.saves.Fillers },
                { _removeStrategy, YG2.saves.Removers },
                { breakStrategy, YG2.saves.Breakers }
        };

        BuffBag buffBag = new(buffs);
        UnlockerSaver unlockerSaver = new(buffBag);
        FillerSaver fillerSaver = new(buffBag);
        RemoverSaver removerSaver = new(buffBag);
        BreakerSaver breakerSaver = new(buffBag);

        _disposer.Add(unlockerSaver);
        _disposer.Add(fillerSaver);
        _disposer.Add(removerSaver);
        _disposer.Add(breakerSaver);

        containerBuilder.AddSingleton(buffBag);
        _buffInitializer.Initialize(_unlockHolderStrategy, _fillHolderStrategy, breakStrategy, _removeStrategy);
    }

    private void InstallClickReaders(ContainerBuilder containerBuilder)
    {
        containerBuilder.AddSingleton(_boltClickReader);
        containerBuilder.AddSingleton(_figureClickReader);

        _figureClickReader.SetPause(true);
    }
}