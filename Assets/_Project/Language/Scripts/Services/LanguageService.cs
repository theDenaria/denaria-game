using _Project.Language.Scripts.Enums;
using _Project.Language.Scripts.Models;
using _Project.Language.Scripts.Signals;
using _Project.LoggingAndDebugging;
using _Project.Utilities.NestedScriptableObject.CustomNestedScriptableObjects;

namespace _Project.Language.Scripts.Services
{
    public class LanguageService : ILanguageService
    {
        [Inject] public ILanguageModel LanguageModel { get; set; }
        [Inject] public LanguageChangedSignal LanguageChangedSignal { get; set; }
        public Languages CurrentLanguage { get; set; } = Languages.ENGLISH;

        public string GetTextBy(string key)
        {
            TranslatableTextListModel translatableTextListModel = LanguageModel.GetLanguagesModel();
            foreach (TranslatableTextModel translatableTextModel in translatableTextListModel.translatableTextModelList)
            {
                if (translatableTextModel.Key == key)
                {
                    return translatableTextModel.Translations[CurrentLanguage]; //TODO: Use Chosen language instead.
                }
            }
            
            DebugLoggerMuteable.LogError("Missing value of key:" + key);
            return key;
            //return "LoginScreen${0}${1}";
        }

        public Languages GetCurrentLanguage()
        {
            return CurrentLanguage;
        }

        public void SetCurrentLanguage(Languages language)
        {
            if (language != CurrentLanguage)
            {
                CurrentLanguage = language;
                LanguageChangedSignal.Dispatch();
            }
        }
    }
}