using System.Collections.Generic;
using _Project.Language.Scripts.ScriptableObjects.Resources;

namespace _Project.Language.Scripts.Models
{
    public class LanguageModel : ILanguageModel
    {
        public List<LanguageScriptableObject> Languages = new ();

        public void FillLanguagesModel(List<LanguageScriptableObject> languages)
        {
            
        }
    }
}