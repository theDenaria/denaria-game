using _Project.Language.Scripts.Enums;
using _Project.Language.Scripts.Models;
using _Project.LoggingAndDebugging;
using _Project.Utilities.NestedScriptableObject.CustomNestedScriptableObjects;

namespace _Project.Language.Scripts.Services
{
    public class LanguageService : ILanguageService
    {
        [Inject] public ILanguageModel LanguageModel { get; set; }

        public string GetTextBy(string key)
        {
            TranslatableTextListModel translatableTextListModel = LanguageModel.GetLanguagesModel();
            foreach (TranslatableTextModel translatableTextModel in translatableTextListModel.translatableTextModelList)
            {
                if (translatableTextModel.Key == key)
                {
                    return translatableTextModel.Translations[Languages.ENGLISH]; //TODO: Use Chosen language instead.
                }
            }
            
            DebugLoggerMuteable.LogError("Missing value of key:" + key);
            return key;
            //return "LoginScreen${0}${1}";
        }
    }
}