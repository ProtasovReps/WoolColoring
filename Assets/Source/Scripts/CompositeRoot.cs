using System.Collections.Generic;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private PictureView _pictureView;
    [SerializeField] private EffectPool _effectPool;
    [SerializeField] private Painter _painter;
    [SerializeField] private Malbert _malbert;
    [SerializeField] private BoltClickReader _clickView;
    [SerializeField] private WhiteStringHolderView _whiteStringHolderView;
    [SerializeField] private ConveyerPosition[] _conveyerPositions;
    [SerializeField] private ColoredStringHolderView[] _stringHolderViews;
    [SerializeField] private FigureFactory _figureFactory;
    [SerializeField] private FigureCompositionFactory _figureCompositionFactory;
    [SerializeField] private BoltHolderRopeConnector _boltConnector;
    [SerializeField] private HoldersRopeConnector _holdersRopeConnector;
    [SerializeField, Min(1)] private int _minFiguresCount;
    [SerializeField, Range(1, 4)] private int _startHoldersCount;

    private Picture _picture;
    private ColoredStringHolderStash _coloredStringHolderStash;
    private ColoredStringHolderSwitcher _switcher;
    private StringDistributor _stringDistributor;
    private BoltStash _boltStash;

    private void Start()
    {
        BindFigures();
        BindPicture();
        BindHolders();
        BindBolt();
        BindRopeConnectors();

        _painter.Initialize(_picture, _switcher, _coloredStringHolderStash);
    }

    private void BindFigures()
    {
        _boltStash = new BoltStash();

        _effectPool.Initialize(_boltStash);
        _figureFactory.Initialize(_boltStash);
        _figureCompositionFactory.Initialize(_figureFactory);

        int minFiguresCount = Mathf.Clamp(_minFiguresCount, 0, _conveyerPositions.Length - 1);
        var conveyer = new Conveyer(_figureCompositionFactory, _conveyerPositions, minFiguresCount);
    }

    private void BindPicture()
    {
        var pictureBinder = new PictureBinder();
        var colorBlockBinder = new ColorBlockBinder();
        List<ColorBlock> colorBlocks = colorBlockBinder.Bind(_pictureView.ColorBlocks);

        _picture = pictureBinder.Bind(_pictureView, colorBlocks, _malbert);
    }

    private void BindHolders()
    {
        var stringBinder = new StringHolderBinder();
        var holderModels = new ColoredStringHolder[_stringHolderViews.Length];

        for (int i = 0; i < _stringHolderViews.Length; i++)
        {
            ColoredStringHolder holderModel = stringBinder.Bind(_stringHolderViews[i]);

            holderModels[i] = holderModel;
        }

        WhiteStringHolder whiteHolderModel = stringBinder.Bind(_whiteStringHolderView, _picture);

        _coloredStringHolderStash = new ColoredStringHolderStash(holderModels, _startHoldersCount);
        _switcher = new ColoredStringHolderSwitcher(_picture, _coloredStringHolderStash);
        _stringDistributor = new StringDistributor(_coloredStringHolderStash, whiteHolderModel, _switcher);

        foreach (var holder in _coloredStringHolderStash.ColoredStringHolders)
        {
            _switcher.ChangeStringHolderColor(holder as ColoredStringHolder);
        }
    }

    private void BindBolt()
    {
        var boltPressPresenter = new BoltPressHandler(_stringDistributor);
        var boltColorPresenter = new BoltColorSetter(_boltStash, _picture);

        _clickView.Initialize(boltPressPresenter);
    }

    private void BindRopeConnectors()
    {
        _boltConnector.Initialize(_stringDistributor);
        _holdersRopeConnector.Initialize(_stringDistributor);
    }
}
