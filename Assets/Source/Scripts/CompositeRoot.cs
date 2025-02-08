using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private PictureView _pictureView;
    [SerializeField] private BoltClickReader _clickView;
    [SerializeField] private StringHolderView _whiteStringHolderView;
    [SerializeField] private ConveyerPosition[] _conveyerPositions;
    [SerializeField] private ColoredStringHolderView[] _stringHolderViews;
    [SerializeField] private FigureFactory _figureFactory;
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

        var painter = new Painter(_picture, _switcher, _coloredStringHolderStash);
    }

    private void BindFigures()
    {
        _boltStash = new BoltStash();

        _figureFactory.Initialize(_boltStash);

        int minFiguresCount = Mathf.Clamp(_minFiguresCount, 0, _conveyerPositions.Length - 1);
        var conveyer = new FigureConveyer(_figureFactory, _conveyerPositions, minFiguresCount);
    }

    private void BindPicture()
    {
        var pictureBinder = new PictureBinder();
        var colorBlockBinder = new ColorBlockBinder();
        List<ColorBlock> colorBlocks = colorBlockBinder.Bind(_pictureView.ColorBlocks);

        _picture = pictureBinder.Bind(_pictureView, colorBlocks);
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

        WhiteStringHolder whiteHolderModel = stringBinder.Bind(_whiteStringHolderView);

        _coloredStringHolderStash = new ColoredStringHolderStash(holderModels, _startHoldersCount);
        _stringDistributor = new StringDistributor(_coloredStringHolderStash, whiteHolderModel);
        _switcher = new ColoredStringHolderSwitcher();

        var colorPresenter = new StringHolderColorPresenter(_coloredStringHolderStash, _picture);

        foreach (var holder in _coloredStringHolderStash.ColoredStringHolders)
        {
            SetStartHolderColor(holder as ColoredStringHolder);
        }
    }

    private void BindBolt()
    {
        var boltPressPresenter = new BoltPressPresenter(_clickView, _stringDistributor);
        var boltColorPresenter = new BoltColorSetter(_boltStash, _picture);

        _clickView.Initialize(boltPressPresenter);
    }

    private void SetStartHolderColor(ColoredStringHolder holder)
    {
        Color requiredColor = _picture.GetRequiredColor();

        _switcher.Switch(requiredColor, holder);
    }
}
