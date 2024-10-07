using _Project.GameSceneManager.Scripts.Signals;
using _Project.MainMenu.Scripts.Signals;
using _Project.MainMenu.Scripts.Views;
using strange.extensions.mediation.impl;

namespace _Project.MainMenu.Scripts.Views
{
    public class MainMenuMediator : Mediator
    {
        [Inject] public MainMenuView View { get; set; }
        [Inject] public MainMenuOpenedSignal MainMenuOpenedSignal { get; set; }
        [Inject] public MainMenuClosedSignal MainMenuClosedSignal { get; set; }
        //[Inject] public PlayerEscMenuInputSignal PlayerEscMenuInputSignal { get; set; }

        public override void OnRegister()
        {

        }

        public override void OnRemove()
        {

        }

        [ListensTo(typeof(PlayerEscMenuInputSignal))]
        public void HandlePlayerEscMenuInput()
        {
            if (View.MainMenuCanvas.gameObject.activeSelf)
            {
                View.MainMenuCanvas.gameObject.SetActive(false);
                MainMenuClosedSignal.Dispatch();
            }
            else
            {
                View.MainMenuCanvas.gameObject.SetActive(true);
                MainMenuOpenedSignal.Dispatch();
            }
            Debug.Log("MainMenuMediator: HandlePlayerEscMenuInput");
        }
    }
}