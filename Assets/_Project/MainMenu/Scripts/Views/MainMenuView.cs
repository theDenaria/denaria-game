using _Project.StrangeIOCUtility;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UIElements;

namespace _Project.MainMenu.Scripts.Views
{
    public class MainMenuView : ViewZeitnot
    {
        [field: SerializeField] public GameObject MainMenuPanel { get; set; }
        [field: SerializeField] public GameObject SettingsPanel { get; set; }
        [field: SerializeField] private ButtonZeitnot ResumeButton { get; set; }
        [field: SerializeField] private ButtonZeitnot SettingsButton { get; set; }
        [field: SerializeField] private ButtonZeitnot LogoutButton { get; set; }
        [field: SerializeField] private ButtonZeitnot ExitButton { get; set; }

        internal Signal onResumeButtonClicked = new Signal();
        internal Signal onSettingsButtonClicked = new Signal();
        internal Signal onLogoutButtonClicked = new Signal();
        internal Signal onExitButtonClicked = new Signal();

        private void OnEnable()
        {
            ResumeButton.onClick.AddListener(OnResumeButtonClicked);
            SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
            LogoutButton.onClick.AddListener(OnLogoutButtonClicked);
            ExitButton.onClick.AddListener(OnExitButtonClicked);
        }

        private void OnDisable()
        {
            ResumeButton.onClick.RemoveListener(OnResumeButtonClicked);
            SettingsButton.onClick.RemoveListener(OnSettingsButtonClicked);
            LogoutButton.onClick.RemoveListener(OnLogoutButtonClicked);
            ExitButton.onClick.RemoveListener(OnExitButtonClicked);
        }

        public void OnResumeButtonClicked()
        {
            Debug.Log("MainMenuView: OnResumeButtonClicked");
            onResumeButtonClicked.Dispatch();
        }

        public void OnSettingsButtonClicked()
        {
            Debug.Log("MainMenuView: OnSettingsButtonClicked");
            onSettingsButtonClicked.Dispatch();
        }

        public void OnLogoutButtonClicked()
        {
            onLogoutButtonClicked.Dispatch();
        }

        public void OnExitButtonClicked()
        {
            onExitButtonClicked.Dispatch();
        }
    }
}