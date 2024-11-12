using System;
using System.Collections.Generic;
using _Project.Language.Scripts.Services;
using _Project.Language.Scripts.Signals;
using _Project.Language.Scripts.Utility;
using _Project.LoggingAndDebugging;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace _Project.Language.Scripts.Views
{
    [RequireComponent(typeof(LanguageView))]
    public class LanguageMediator : Mediator
    {
        [SerializeField] private string _key;

        //Keeps the special regex string parts like $(0), that will be dynamically filled.
        //Key of the dictionary holds special regex string parts like $(0), and value holds the string that will be shown.
        [SerializeField] private Dictionary<string, string> _wildStringDictionary;

        private LanguageView _languageView;
        [Inject] private static ILanguageService LanguageService { get; set; }

        private static bool _isInitialized;//TODO: This state should not depend on view layer
        private bool _isDependenciesCached;

        private void OnEnable()
        {
            if (!_isInitialized)
            {
                return;
            } //For the objects that were asleep when all services are initialized.

            FillTextByKey(_key);
        }
        
        [ListensTo(typeof(LanguageServiceInitializedSignal))]
        public void HandleAllServicesInitialized()
        {
            _isInitialized = true;
            FillTextByKey(_key);
        }

        public void FillTextByKey(string key, Dictionary<string, string> wildStringDictionary = null)
        {
            if (!_isDependenciesCached)
            {
                CacheDependencies();
            }

            SetLanguageKey(key);
            SetWildStringDictionary(wildStringDictionary);

            string rawText = GetRawText(key);

            string filledText = FillWildStrings(rawText, wildStringDictionary);

            _languageView?.Set(filledText);
        }

        private void CacheDependencies()
        {
            //LanguageService ??= GetService<ILanguageService>();

            _languageView = GetComponent<LanguageView>();
            if (_languageView == null)
            {
                DebugLoggerMuteable.LogWarning("LanguageView is null. key was: " + _key);
                return;
            }

            _isDependenciesCached = true;
        }

        private void SetLanguageKey(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                DebugLoggerMuteable.LogWarning("we are waiting language key in:" + gameObject.name);
                return;
            }

            _key = key;
        }

        private void SetWildStringDictionary(Dictionary<string, string> wildStringDictionary)
        {
            if (wildStringDictionary == null)
            {
                DebugLoggerMuteable.LogWarning("wildStringDictionary null in:" + gameObject.name);
                return;
            }

            _wildStringDictionary = wildStringDictionary;
        }

        private string GetRawText(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                return "";
            }

            if (key.IsNumeric())
            {
                return key;
            }

            //_languageService ??= GetService<ILanguageService>();

            string rawText = LanguageService.GetTextBy(key);

            if (String.IsNullOrEmpty(rawText))
            {
                DebugLoggerMuteable.LogWarning("rawText null for key: " + _key);
                return "";
            }

            return rawText;
        }

        private string FillWildStrings(string rawText, Dictionary<string, string> wildStringDictionary)
        {
            string filledText = rawText;

            if (wildStringDictionary != null)
            {
                foreach (KeyValuePair<string, string> item in wildStringDictionary)
                {
                    filledText = rawText.FillParams(new Dictionary<string, string>()
                    {
                        { item.Key, item.Value }
                    });
                }
            }

            if (String.IsNullOrEmpty(filledText))
            {
#if UNITY_EDITOR
                DebugLoggerMuteable.LogWarning("filledText null for key: " + _key);
#endif

                return "";
            }

            return filledText;
        }

        [ListensTo(typeof(LanguageChangedSignal))]
        public void HandleLanguageChanged()
        {
            FillTextByKey(_key, _wildStringDictionary);
        }
    }
}