using System;
using System.Collections.Generic;
using UnityEngine;
using Reflex.Core;
using Ami.BroAudio;
using StringHolders;
using StringHolders.Model;
using StringHolders.View;
using BlockPicture;
using BlockPicture.Model;
using BlockPicture.View;
using ColorStrings;
using ColorStrings.Model;
using Buffs;
using Buffs.Strategies;
using Bolts;
using ColorBlocks;
using ColorBlocks.View;
using FigurePlatform;
using FigurePlatform.Model;
using FigurePlatform.View;
using Extensions;
using ViewExtensions;
using Input;
using ConnectingRope;
using ClickReaders;
using PlayerWallet;
using LevelInterface;
using LevelInterface.Initializers;
using LevelInterface.Blocks;
using LevelInterface.Timers;
using CustomInterface;
using YG;
using YandexGamesSDK.Saves;
using YandexGamesSDK.Saves.Buffs;
using YandexGamesSDK.Saves.PlayerWallet;
using YandexGamesSDK.Saves.Level;
using YandexGamesSDK.Metrics;
using YandexGamesSDK.Leaderboard;
using YandexGamesSDK.Inaps;

namespace DependencyInjection
{
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
        [SerializeField] private UIAnimations _uIAnimator;
        [SerializeField] private LevelTransitionAnimation _levelTransitionAnimation;
        [SerializeField] private Store _store;
        [Header("Disposer")]
        [SerializeField] private ObjectDisposer _disposer;
        [Header("AdTimer")]
        [SerializeField] private float _coinAdCooldownTime;

        private BoltColorSetter _boltColorSetter;
        private Conveyer _conveyer;
        private Unlocker _unlocker;
        private Filler _filler;
        private Remover _remover;
        private Breaker _breaker;
        private Stopwatch _stopwatch;

        private void Start()
        {
            _levelTransitionAnimation.FadeIn();
            _conveyer.FillAllFigures();
            _boltColorSetter.SetColors();
            _activatableInitializator.Initialize();
            BroAudio.Play(_soundID);
            _stopwatch.StartCount().Forget();
        }

        public void InstallBindings(ContainerBuilder containerBuilder)
        {
            InstallFigureComposition();

            Picture picture = InstallPicture(containerBuilder);
            StringDistributor stringDistributor = InstallHolders(containerBuilder, picture);
            Wallet wallet = InstallWallet(containerBuilder, picture);
            BuffBag buffBag = InstallBuffs(containerBuilder);
            ProgressSaver saver = InstallSavers(wallet, buffBag, picture, containerBuilder);
            _stopwatch = new Stopwatch();
            PlayerInput input = new();

            InstallBolt(containerBuilder, stringDistributor, picture);
            InstallRopeConnector(containerBuilder);
            InstallClickReaders(containerBuilder);
            InstallYandex(picture, wallet, saver);
            InstallTimers(containerBuilder);
            input.PlayerClick.Enable();

            containerBuilder.AddSingleton(input);
            containerBuilder.AddSingleton(_levelTransitionAnimation);
            containerBuilder.AddSingleton(_uIAnimator);
            containerBuilder.AddSingleton(_store);
            containerBuilder.AddSingleton(_stopwatch);
        }

        private void InstallFigureComposition()
        {
            var positionDatabase = new PositionDatabase(_conveyerPositions);
            var compositionPool = new FigureCompositionPool(_figureCompositionFactory);

            _conveyer = new Conveyer(compositionPool, positionDatabase);

            _figureCompositionFactory.Initialize(_disposer);
            _disposer.Add(_conveyer);
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

            _unlocker = new(coloredStringHolderStash, switcher, picture);
            _filler = new(coloredHolderModels);
            _remover = new(whiteHolderModel);

            _disposer.Add(stringRemover);
            _painter.Initialize(coloredStringHolderStash, switcher);
            containerBuilder.AddSingleton(whiteHolderModel);
            containerBuilder.AddSingleton(stringDistributor);
            containerBuilder.AddSingleton(_coloredViews);
            containerBuilder.AddSingleton(_whiteStringHolderView);
            containerBuilder.AddSingleton(coloredStringHolderStash);

            foreach (var holder in coloredStringHolderStash.ActiveHolders)
                switcher.Switch(holder as ColoredStringHolder);

            return stringDistributor;
        }

        private void InstallRopeConnector(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_ropePool);
        }

        private Wallet InstallWallet(ContainerBuilder containerBuilder, Picture picture)
        {
            var wallet = new Wallet(YG2.saves.Coins);
            var moneyRewards = new MoneyRewards(picture, wallet, _figureCompositionFactory);

            _disposer.Add(moneyRewards);
            containerBuilder.AddSingleton(wallet, typeof(ICountChangeable), typeof(Wallet));
            return wallet;
        }

        private BuffBag InstallBuffs(ContainerBuilder containerBuilder)
        {
            _breaker = new Breaker(_figureClickReader);
            var buffs = new Dictionary<IBuff, int>()
            {
                { _unlocker, YG2.saves.Unlockers },
                { _filler, YG2.saves.Fillers },
                { _remover, YG2.saves.Removers },
                { _breaker, YG2.saves.Breakers }
            };

            BuffBag buffBag = new(buffs);

            containerBuilder.AddSingleton(_unlocker);
            containerBuilder.AddSingleton(_filler);
            containerBuilder.AddSingleton(_remover);
            containerBuilder.AddSingleton(_breaker);
            containerBuilder.AddSingleton(buffBag);
            return buffBag;
        }

        private ProgressSaver InstallSavers(Wallet wallet, BuffBag buffBag, Picture picture, ContainerBuilder containerBuilder)
        {
            ISaver[] savers =
            {
                new UnlockerSaver(buffBag),
                new FillerSaver(buffBag),
                new RemoverSaver(buffBag),
                new BreakerSaver(buffBag),
                new WalletSaver(wallet),
                new LevelSaver(),
                new UnlockedLevelsSaver(),
            };

            ProgressSaver progressSaver = new(picture, savers);

            containerBuilder.AddSingleton(progressSaver);
            _disposer.Add(progressSaver);
            return progressSaver;
        }

        private void InstallTimers(ContainerBuilder containerBuilder)
        {
            var coinAdTimer = new CoinAdTimer(_coinAdCooldownTime);

            containerBuilder.AddSingleton(coinAdTimer);
            _disposer.Add(coinAdTimer);
        }

        private void InstallClickReaders(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddSingleton(_boltClickReader);
            containerBuilder.AddSingleton(_figureClickReader);

            _figureClickReader.SetPause(true);
        }

        private void InstallYandex(Picture picture, Wallet wallet, ProgressSaver progressSaver)
        {
            AdWatchedMetrics metrics = new();
            BlocksColoredLeaderboard leaderboard = new(picture);
            InapCoinsAdder coinsAdder = new(wallet);
            AdsRemover adsRemover = new();
            InapBuffAdder inapBuffAdder = new(wallet, _store, _unlocker, _filler, _remover, _breaker);
            SuperDealPurchase superDealPurchase = new(progressSaver, inapBuffAdder);
            StarterPackPurchase starterPackPurchase = new(coinsAdder, inapBuffAdder, adsRemover, progressSaver);
            RemoveAdsPackPurchase removeAdsPurchase = new(progressSaver, adsRemover);
            InapCoinsPack coinsPack = new(progressSaver, coinsAdder);

            if (YG2.saves.IfAdsRemoved)
                YG2.StickyAdActivity(false);

            _disposer.Add(metrics);
            _disposer.Add(leaderboard);
            _disposer.Add(superDealPurchase);
            _disposer.Add(starterPackPurchase);
            _disposer.Add(removeAdsPurchase);
            _disposer.Add(coinsPack);
        }
    }
}