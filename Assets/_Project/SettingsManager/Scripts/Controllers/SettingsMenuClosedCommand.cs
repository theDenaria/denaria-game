using _Project.SettingsManager.Scripts.Enums;
using _Project.SettingsManager.Scripts.Views;
using strange.extensions.command.impl;

namespace _Project.SettingsManager.Scripts.Controllers
{
    public class SettingsMenuClosedCommand : Command
    {
        [Inject] public FooterView View { get; set; }
        public override void Execute()
        {
            //View.SettingsPanel.SetActive(false);
        }
    }
}