using ColorStrings.Model;
using Reflex.Attributes;
using StringHolders.Model;
using UnityEngine;

namespace PlayerGuide
{
    public class RemoverReplic : BuffReplic
    {
        private const int AddStringCount = 3;

        [SerializeField] private Color _stringColor;

        private WhiteStringHolder _whiteStringHolder;

        [Inject]
        private void Inject(WhiteStringHolder holder)
        {
            _whiteStringHolder = holder;
        }

        public override void Activate()
        {
            var fakeString = new ColorString();

            fakeString.SetColor(_stringColor);

            for (int i = 0; i < AddStringCount; i++)
            {
                _whiteStringHolder.Add(fakeString);
            }

            base.Activate();
        }
    }
}