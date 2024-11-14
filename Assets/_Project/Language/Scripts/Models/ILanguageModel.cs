using System.Collections.Generic;
using _Project.Utilities.NestedScriptableObject.CustomNestedScriptableObjects;

namespace _Project.Language.Scripts.Models
{
    public interface ILanguageModel
    {
        public void FillLanguagesModel(TranslatableTextListModel languages);
        public TranslatableTextListModel GetLanguagesModel();
    }
}