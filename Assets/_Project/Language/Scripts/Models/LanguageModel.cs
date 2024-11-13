using System.Collections.Generic;
using _Project.Utilities.NestedScriptableObject.CustomNestedScriptableObjects;

namespace _Project.Language.Scripts.Models
{
    public class LanguageModel : ILanguageModel
    {
        public TranslatableTextListModel TranslatableTextListModel;

        public void FillLanguagesModel(TranslatableTextListModel languages)
        {
            TranslatableTextListModel = languages;
        }

        public TranslatableTextListModel GetLanguagesModel()
        {
            return TranslatableTextListModel;
        }
    }
}