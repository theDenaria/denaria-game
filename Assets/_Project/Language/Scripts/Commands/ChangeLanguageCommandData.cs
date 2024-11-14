using _Project.Language.Scripts.Enums;

namespace _Project.Language.Scripts.Commands
{
    public class ChangeLanguageCommandData
    {
        public Languages Language { get; set; }

        public ChangeLanguageCommandData(Languages language)
        {
            Language = language;
        }
    }
}