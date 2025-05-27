using Reflex.Attributes;
using StringHolderSystem.Model;

namespace UISystem.Blocks
{
    public class LoseBlock : ActivatableUI
    {
        private WhiteStringHolder _whiteStringHolder;

        [Inject]
        private void Inject(WhiteStringHolder whiteStringHolder)
        {
            _whiteStringHolder = whiteStringHolder;
            _whiteStringHolder.Filled += OnFilled;
        }

        private void OnDestroy()
        {
            _whiteStringHolder.Filled -= OnFilled;
        }

        private void OnFilled(WhiteStringHolder holder)
        {
            Activate();
        }
    }
}