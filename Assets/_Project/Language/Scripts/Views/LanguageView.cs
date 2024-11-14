using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Language.Scripts.Commands;
using _Project.Language.Scripts.Enums;
using _Project.Language.Scripts.Services;
using _Project.Language.Scripts.Signals;
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
        internal Signal<ChangeLanguageCommandData> changeLanguageViewSignal = new Signal<ChangeLanguageCommandData>();
        
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
        
        protected void Start()
        {
            base.Start();
            StartCoroutine(TestLanguageChange());
        }

        private IEnumerator TestLanguageChange()
        {
            Languages language = Languages.ENGLISH;

            while (true)
            {
                yield return new WaitForSeconds(1f);

                Debug.Log("xxx changeLanguageViewSignal");
                changeLanguageViewSignal.Dispatch(new ChangeLanguageCommandData(language));

                // Alternate between 1 and 2 for the next dispatch
                language = language == Languages.ENGLISH ? Languages.TURKISH : Languages.ENGLISH;

                // Wait for 3 seconds before the next dispatch
                yield return new WaitForSeconds(2f);
            }
        }
    }
}