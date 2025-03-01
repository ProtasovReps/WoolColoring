using Reflex.Attributes;
using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private Painter _painter;
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
    private Conveyer _conveyer;

    [Inject]
    private void Inject(Conveyer conveyer, Picture picture)
    {
        _conveyer = conveyer;
        _picture = picture;
    }

    private void Start()
    {
        SetupFigures();
        BindHolders();
        BindBolt();
        BindRopeConnectors();

        _painter.Initialize(_picture, _switcher, _coloredStringHolderStash);
    }

    private void SetupFigures()
    {
        _conveyer.FillAllFigures();
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
