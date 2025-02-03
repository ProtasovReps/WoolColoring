using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private PictureView _pictureView;
    [SerializeField] private PlayerClickView _clickView;
    [SerializeField] private StringHolderView _whiteStringHolder;
    [SerializeField] private ColoredStringHolderView[] _stringHolders;
    [SerializeField] private BoltStash _boltStash;
    [SerializeField, Range(1, 4)] private int _startHoldersCount;

    private Picture _picture;
    private ColoredStringHolderStash _coloredStringHolderStash;
    private ColoredStringHolderSwitcher _switcher;
    private StringDistributor _stringDistributor;

    private void Awake()
    {
        BindPicture();
        BindHolders();
        BindBolt();

        var painter = new Painter(_picture, _switcher, _coloredStringHolderStash);
    }

    private void BindPicture()
    {
        var pictureBinder = new PictureBinder();

        _picture = pictureBinder.Bind(_pictureView, new ColorBlockBinder());
    }

    private void BindHolders()
    {
        var stringBinder = new StringHolderBinder();
        var holders = new ColoredStringHolder[_stringHolders.Length];

        for (int i = 0; i < _stringHolders.Length; i++)
        {
            ColoredStringHolder holder = stringBinder.Bind(_stringHolders[i]);

            holders[i] = holder;
        }

        WhiteStringHolder whiteHolder = stringBinder.Bind(_whiteStringHolder);

        _coloredStringHolderStash = new ColoredStringHolderStash(holders, _startHoldersCount);
        _stringDistributor = new StringDistributor(_coloredStringHolderStash, whiteHolder);
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
        var boltColorPresenter = new BoltColorPresenter(_boltStash, _picture);

        _clickView.Initialize(boltPressPresenter);
    }

    private void SetStartHolderColor(ColoredStringHolder holder)
    {
        Color requiredColor = _picture.GetRequiredColor();

        _switcher.Switch(requiredColor, holder);
    }
}
