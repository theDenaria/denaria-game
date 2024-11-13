using System;
using System.Collections.Generic;
using _Project.Language.Scripts.Services;
using _Project.Language.Scripts.Utility;
using _Project.LoggingAndDebugging;
using strange.extensions.command.impl;
using UnityEngine.InputSystem;

namespace _Project.Language.Scripts.Commands
{
    public class FillTextByKeyCommand : Command
    {
        [Inject] public FillTextByKeyCommandData FillTextByKeyCommandData { get; set; }
        [Inject] public ILanguageService LanguageService { get; set; }
        public string Key { get; set; }
        public Dictionary<string, string> WildStringDictionary { get; set; }

        public override void Execute()
        {
            FillTextByKey();
        }
        
        public void FillTextByKey()
        {
            SetLanguageKey(FillTextByKeyCommandData.Key);
            SetWildStringDictionary(FillTextByKeyCommandData.WildStringDictionary);

            string rawText = GetRawText(Key);

            string filledText = FillWildStrings(rawText, WildStringDictionary);

            FillTextByKeyCommandData.View.Set(filledText);
        }

        private void SetLanguageKey(string key)
        {
            if (String.IsNullOrEmpty(key))
            {
                DebugLoggerMuteable.LogWarning("we are waiting language key in:" + FillTextByKeyCommandData.View.gameObject.name);
                return;
            }

            Key = FillTextByKeyCommandData.Key;
        }

        private void SetWildStringDictionary(Dictionary<string, string> wildStringDictionary)
        {
            if (wildStringDictionary == null)
            {
                DebugLoggerMuteable.LogWarning("wildStringDictionary null in:" + FillTextByKeyCommandData.View.gameObject.name);
                return;
            }

            WildStringDictionary = wildStringDictionary;
        }

        private string GetRawText(string key)
        {
            //return "LoginScreen$(0)$(1)";
            if (String.IsNullOrEmpty(key))
            {
                return "";
            }

            if (key.IsNumeric())
            {
                return key;
            }

            //_languageService ??= GetService<ILanguageService>();
            string rawText = "";
            rawText = LanguageService.GetTextBy(key);

            if (String.IsNullOrEmpty(rawText))
            {
                DebugLoggerMuteable.LogWarning("rawText null for key: " + Key);
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
                DebugLoggerMuteable.LogWarning("filledText null for key: " + FillTextByKeyCommandData.Key);
#endif

                return "";
            }

            return filledText;
        }
    }
}