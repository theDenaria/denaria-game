using System;
using System.Collections.Generic;
using _Project.Language.Scripts.Services;
using _Project.Language.Scripts.Utility;
using _Project.LoggingAndDebugging;
using _Project.StrangeIOCUtility.Scripts.Views;
using strange.extensions.signal.impl;
using TMPro;
using UnityEngine;

namespace _Project.Language.Scripts.Views
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LanguageView : ViewZeitnot
    {
        [Inject] private static ILanguageService LanguageService { get; set; }

        [SerializeField] internal string Key { get; set; }
        
        //Keeps the special regex string parts like $(0), that will be dynamically filled.
        //Key of the dictionary holds special regex string parts like $(0), and value holds the string that will be shown.
        [SerializeField] internal Dictionary<string, string> WildStringDictionary { get; set; }

        private TextMeshProUGUI _textMeshProUGUI;
        internal Signal onViewInitialized = new Signal();

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

        public void Init()
        {
            //FillTextByKey();
        }
    }
}