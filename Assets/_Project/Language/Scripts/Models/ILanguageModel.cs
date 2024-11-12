using System.Collections.Generic;
using _Project.Language.Scripts.ScriptableObjects.Resources;

namespace _Project.Language.Scripts.Models
{
    public interface ILanguagesModel
    {
        public void FillLanguagesModel(List<LanguageScriptableObject> guns);
    }
}