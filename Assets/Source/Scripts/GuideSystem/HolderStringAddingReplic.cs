using UnityEngine;
using ClickReadingSystem;

namespace GuideSystem
{
    public class HolderStringAddingReplic : Replic
    {
        [SerializeField] private GuideBoltClickReader _clickReader;
        [SerializeField] private bool _isWhiteStringHolderGuide;

        public override void Activate()
        {
            base.Activate();
            _clickReader.SetWhiteStringHolderGuide(_isWhiteStringHolderGuide);
            _clickReader.SetPause(false);
            _clickReader.Unscrewed += OnUnscrewed;
        }

        private void OnUnscrewed()
        {
            _clickReader.SetPause(true);
            _clickReader.Unscrewed -= OnUnscrewed;
            Deactivate();
        }
    }
}