using _Project.GameSceneManager.Scripts.Signals;
using _Project.MainMenu.Scripts.Signals;
using _Project.MainMenu.Scripts.Views;
using strange.extensions.mediation.impl;
using UnityEngine;

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
            View.onSettingsButtonClicked.AddListener(HandleSettingsButton);
            View.onResumeButtonClicked.AddListener(HandleResumeButton);
            View.onLogoutButtonClicked.AddListener(HandleLogoutButton);
            View.onExitButtonClicked.AddListener(HandleExitButton);
        }

        public override void OnRemove()
        {
            View.onSettingsButtonClicked.RemoveListener(HandleSettingsButton);
            View.onResumeButtonClicked.RemoveListener(HandleResumeButton);
            View.onLogoutButtonClicked.RemoveListener(HandleLogoutButton);
            View.onExitButtonClicked.RemoveListener(HandleExitButton);
        }

        [ListensTo(typeof(PlayerEscMenuInputSignal))]
        public void HandlePlayerEscMenuInput()
        {
            if (View.MainMenuPanel.gameObject.activeSelf)
            {
                View.MainMenuPanel.gameObject.SetActive(false);
                MainMenuClosedSignal.Dispatch();
            }
            else
            {
                View.MainMenuPanel.gameObject.SetActive(true);
                MainMenuOpenedSignal.Dispatch();
            }
            Debug.Log("MainMenuMediator: HandlePlayerEscMenuInput");
        }

        public void HandleResumeButton()
        {
            Debug.Log("MainMenuMediator: HandleResumeButton");
            View.MainMenuPanel.SetActive(false);
            MainMenuClosedSignal.Dispatch();
        }

        public void HandleSettingsButton()
        {
            Debug.Log("MainMenuMediator: HandleSettingsButton");
            View.MainMenuPanel.SetActive(false);
            View.SettingsPanel.SetActive(true);

        }

        public void HandleLogoutButton()
        {
            Debug.Log("MainMenuMediator: HandleLogoutButton");

        }

        public void HandleExitButton()
        {
            Debug.Log("MainMenuMediator: HandleExitButton");
            Application.Quit();
        }
    }
}