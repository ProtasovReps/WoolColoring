using Reflex.Attributes;

public class LoseBlock : ActivatableUI
{
    private WhiteStringHolder _whiteStringHolder;

    private void OnDestroy()
    {
        _whiteStringHolder.Filled -= OnFilled;
    }

    [Inject]
    private void Inject(WhiteStringHolder whiteStringHolder)
    {
        _whiteStringHolder = whiteStringHolder;
        _whiteStringHolder.Filled += OnFilled;
    }

    private void OnFilled(WhiteStringHolder holder) => Activate();
}