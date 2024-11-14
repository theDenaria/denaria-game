using _Project.Language.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.Language.Scripts.Commands
{
    public class ChangeLanguageCommand : Command
    {
        [Inject] public ILanguageService LanguageService { get; set; }
        [Inject] public ChangeLanguageCommandData ChangeLanguageCommandData { get; set; }
        public override void Execute()
        {
            LanguageService.SetCurrentLanguage(ChangeLanguageCommandData.Language);
        }
    }
}