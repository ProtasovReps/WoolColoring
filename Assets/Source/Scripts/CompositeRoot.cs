using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private Picture _picture;
    [SerializeField] private PlayerClickView _clickView;
    [SerializeField] private StringHolderView _whiteStringHolder;
    [SerializeField] private ColoredStringHolderView[] _stringHolders;
    [SerializeField, Range(1, 4)] private int _startHoldersCount;

    private StringHolderBinder _binder;
    private Painter _painter;
    private ColoredStringHolderStash _stash;
    private ColoredStringHolderSwitcher _switcher;
    private StringDistributor _stringDistributor;
    private BoltPressPresenter _boltPressPresenter;

    private void Awake()
    {
        _picture.Initialize();

        BindHolders();

        _boltPressPresenter = new BoltPressPresenter(_clickView, _stringDistributor);
        _clickView.Initialize(_boltPressPresenter);
        _painter = new Painter(_picture, _switcher, _stash);
    }

    private void BindHolders()
    {
        _binder = new StringHolderBinder();
        var holders = new ColoredStringHolder[_stringHolders.Length];

        for (int i = 0; i < _stringHolders.Length; i++)
        {
            ColoredStringHolder holder = _binder.Bind(_stringHolders[i]);

            holders[i] = holder;
        }

        WhiteStringHolder whiteHolder = _binder.Bind(_whiteStringHolder);

        _stash = new ColoredStringHolderStash(holders, _startHoldersCount);
        _stringDistributor = new StringDistributor(_stash, whiteHolder);
        _switcher = new ColoredStringHolderSwitcher();

        foreach (var holder in _stash.ColoredStringHolders)
        {
            SetStartHolderColor(holder as ColoredStringHolder);
        }
    }

    private void SetStartHolderColor(ColoredStringHolder holder)
    {
        Color requiredColor = _picture.GetRequiredColor();

        _switcher.Switch(requiredColor, holder);
    }
}
