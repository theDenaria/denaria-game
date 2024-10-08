using _Project.MainMenu.Scripts.Signals;
using strange.extensions.command.impl;

namespace _Project.MainMenu.Scripts.Controllers
{
    public class MainMenuToggleCommand : Command
    {

        public bool IsMainMenuVisible { get; set; }

        public override void Execute()
        {
            if (IsMainMenuVisible)
            {
                Debug.Log("MainMenu Visible");
            }
            else
            {
                Debug.Log("MainMenu Hidden");
            }
        }
    }
}