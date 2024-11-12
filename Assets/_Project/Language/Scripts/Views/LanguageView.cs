using System;
using _Project.LoggingAndDebugging;
using _Project.StrangeIOCUtility.Scripts.Views;
using TMPro;
using UnityEngine;

namespace _Project.Language.Scripts.Views
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LanguageView : ViewZeitnot
    {
        private TextMeshProUGUI _textMeshProUGUI;

        private void OnEnable()
        {
            _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        }

        public void Set(string word)
        {
            _textMeshProUGUI ??= GetComponent<TextMeshProUGUI>(); //Not early enough to do this in OnEnable()

            if (!_textMeshProUGUI)
            {
                DebugLoggerMuteable.LogWarning("_textMeshProUGUI is null in LanguageView.Set(). Word was: " + word + " gameobject was: " + gameObject);
                return;
            }

            if (String.IsNullOrEmpty(word))
            {
                DebugLoggerMuteable.LogWarning("word is null in LanguageView.Set(). _textMeshProUGUI was: " + _textMeshProUGUI + " gameobject was: " + gameObject);
                return;
            }
            
            _textMeshProUGUI.SetText(word);
        }
    }
}