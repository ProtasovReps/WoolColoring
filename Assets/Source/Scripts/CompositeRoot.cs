using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private PictureView _pictureView;
    [SerializeField] private PlayerClickView _clickView;
    [SerializeField] private StringHolderView _whiteStringHolder;
    [SerializeField] private ColoredStringHolderView[] _stringHolders;
    [SerializeField, Range(1, 4)] private int _startHoldersCount;

    private Picture _picture;
    private ColoredStringHolderStash _stash;
    private ColoredStringHolderSwitcher _switcher;
    private StringDistributor _stringDistributor;

    private void Awake()
    {
        BindPicture();
        BindHolders();
        BindBolt();

        var painter = new Painter(_picture, _switcher, _stash);
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

        _stash = new ColoredStringHolderStash(holders, _startHoldersCount);
        _stringDistributor = new StringDistributor(_stash, whiteHolder);
        _switcher = new ColoredStringHolderSwitcher();

        foreach (var holder in _stash.ColoredStringHolders)
        {
            SetStartHolderColor(holder as ColoredStringHolder);
        }
    }

    private void BindBolt()
    {
        var boltPresenter = new BoltPressPresenter(_clickView, _stringDistributor);

        _clickView.Initialize(boltPresenter);
    }

    private void SetStartHolderColor(ColoredStringHolder holder)
    {
        Color requiredColor = _picture.GetRequiredColor();

        _switcher.Switch(requiredColor, holder);
    }
}
