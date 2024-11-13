using System.Collections.Generic;
using _Project.Language.Scripts.Views;

namespace _Project.Language.Scripts.Commands
{
    public class FillTextByKeyCommandData
    {
        public LanguageView View { get; set; }
        public string Key { get; set; }
        public Dictionary<string, string> WildStringDictionary { get; set; }

        public FillTextByKeyCommandData(LanguageView languageView, string key, Dictionary<string, string> wildStringDictionary)
        {
            View = languageView;
            Key = key;
            WildStringDictionary = wildStringDictionary;
        }
    }
}