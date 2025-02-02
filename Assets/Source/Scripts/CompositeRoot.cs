using UnityEngine;

public class CompositeRoot : MonoBehaviour
{
    [SerializeField] private Picture _picture;
    [SerializeField] private Painter _painter;
    [SerializeField] private PlayerClickView _clickView;
    [SerializeField] private WhiteStringHolder _whiteStringHolder;
    [SerializeField] private ColoredStringHolder[] _unlockedStringHolders;
    [SerializeField] private ColoredStringHolder[] _lockedStringHolders;

    private ColoredStringHolderStash _stash;
    private ColoredStringHolderSwitcher _switcher;
    private StringDistributor _stringDistributor;
    private BoltPressPresenter _distributorPresenter;

    private void Awake()
    {
        _stash = new ColoredStringHolderStash(_unlockedStringHolders, _lockedStringHolders);
        _switcher = new ColoredStringHolderSwitcher();
        _stringDistributor = new StringDistributor(_stash, _whiteStringHolder);
        _distributorPresenter = new BoltPressPresenter(_clickView, _stringDistributor);

        _clickView.Initialize(_distributorPresenter);
        _picture.Initialize();
        _painter.Initialize(_switcher, _stash);

        foreach (var holder in _unlockedStringHolders)
        {
            Color requiredColor = _picture.GetRequiredColor();

            holder.Initialize();
            _switcher.Switch(requiredColor, holder);
        }
    }
}
