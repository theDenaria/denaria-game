using System;
using System.Collections.Generic;
using _Project.Language.Scripts.Utility;
using _Project.LoggingAndDebugging;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.mediation.impl;
using UnityEngine;

namespace _Project.Language.Scripts.Views
{
    [RequireComponent(typeof(LanguageTextController))]
    public class LanguageMediator : Mediator
    {
        [SerializeField] private string _key;

        //Keeps the special regex string parts like $(0), that will be dynamically filled.
        //Key of the dictionary holds special regex string parts like $(0), and value holds the string that will be shown.
        [SerializeField] private Dictionary<string, string> _wildStringDictionary;

        private ILanguageTextController _languageTextController;
        private static ILanguageService _languageService;

        private static bool _isInitialized;
        private bool _isDependenciesCached;

        private void OnEnable()
        {
            Subscribe(GameEvents.LANGUAGE_CHANGED, HandleLanguageChanged);
            Subscribe(GameEvents.ALL_SERVICE_INIT_OK, HandleAllServicesInitialized);

            if (!_isInitialized)
            {
                return;
            } //For the objects that were asleep when all services are initialized.

            FillTextByKey(_key);
        }

        private void OnDisable()
        {
            Unsubscribe(GameEvents.LANGUAGE_CHANGED, HandleLanguageChanged);
            Unsubscribe(GameEvents.ALL_SERVICE_INIT_OK, HandleAllServicesInitialized);
        }

        private void HandleAllServicesInitialized(IEvent arg0)
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

            _languageTextController?.Set(filledText);
        }

        private void CacheDependencies()
        {
            _languageService ??= GetService<ILanguageService>();

            _languageTextController = GetComponent<ILanguageTextController>();
            if (_languageTextController == null)
            {
                DebugLoggerMuteable.LogWarning("_languageTextController is null. key was: " + _key);
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

            string rawText = _languageService.GetWord(key);

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

        private void HandleLanguageChanged(IEvent e = null)
        {
            FillTextByKey(_key, _wildStringDictionary);
        }
    }
}