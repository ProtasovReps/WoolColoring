using Reflex.Attributes;
using System.Collections.Generic;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private PictureView _pictureView;
    [SerializeField] private Painter _painter;
    [SerializeField] private Malbert _malbert;
    [SerializeField] private BoltClickReader _clickView;
    [SerializeField] private WhiteStringHolderView _whiteStringHolderView;
    [SerializeField] private ColoredStringHolderView[] _stringHolderViews;
    [SerializeField] private BoltHolderRopeConnector _boltConnector;
    [SerializeField] private HoldersRopeConnector _holdersRopeConnector;
    [SerializeField] private BlockHolderConnector _blockHolderConnector;
    [SerializeField, Range(1, 4)] private int _startHoldersCount;

    private Picture _picture;
    private ColoredStringHolderStash _coloredStringHolderStash;
    private ColoredStringHolderSwitcher _switcher;
    private StringDistributor _stringDistributor;
    [Inject] private readonly Conveyer _conveyer;

    private void Start()
    {
        SetupFigures();
        BindPicture();
        BindHolders();
        BindBolt();
        BindRopeConnectors();

        _painter.Initialize(_picture, _switcher, _coloredStringHolderStash);
    }

    private void SetupFigures()
    {
        _conveyer.FillAllFigures();
    }

    private void BindPicture()
    {
        var pictureBinder = new PictureBinder();
        var colorBlockBinder = new ColorBlockBinder();

        _blockHolderConnector.Initialize();

        List<ColorBlock> colorBlocks = colorBlockBinder.Bind(_pictureView.ColorBlocks, _blockHolderConnector);
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
        //var boltColorPresenter = new BoltColorSetter(_picture);

        _clickView.Initialize(boltPressPresenter);
    }

    private void BindRopeConnectors()
    {
        _boltConnector.Initialize(_stringDistributor);
        _holdersRopeConnector.Initialize(_stringDistributor);
    }
}
