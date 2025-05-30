using System;
using System.Linq;
using Lean.Localization;

namespace UISystem.Buttons
{
    public class LanguageButton : ButtonView
    {
        private string[] _languages;

        private void Awake()
        {
            _languages = LeanLocalization.CurrentLanguages.Keys.ToArray();
        }

        protected override void OnButtonClick()
        {
            int currentLanguageIndex = Array.IndexOf(_languages, LeanLocalization.GetFirstCurrentLanguage());
            int nextIndex = (currentLanguageIndex + 1) % _languages.Length;

            LeanLocalization.SetCurrentLanguageAll(_languages[nextIndex]);
            base.OnButtonClick();
        }
    }
}