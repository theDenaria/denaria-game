using _Project.Language.Scripts.Enums;

namespace _Project.Language.Scripts.Services
{
    public interface ILanguageService
    {
        public string GetTextBy(string key);
        public Languages GetCurrentLanguage();
        public void SetCurrentLanguage(Languages language);
    }
}