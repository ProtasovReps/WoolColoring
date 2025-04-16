using Lean.Localization;
using System;
using System.Linq;
using UnityEngine;

public class LanguageButton : ButtonView
{
    [SerializeField] private LeanLocalization _localisation;

    private string[] _languages;

    private void Awake()
    {
        _languages = LeanLocalization.CurrentLanguages.Keys.ToArray();
    }

    protected override void OnButtonClick()
    {
        int currentLanguageIndex = Array.IndexOf(_languages, _localisation.CurrentLanguage);
        int nextIndex = (currentLanguageIndex + 1) % _languages.Length;

        _localisation.CurrentLanguage = _languages[nextIndex];
        base.OnButtonClick();
    }
}