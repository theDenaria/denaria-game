using System;
using _Project.StrangeIOCUtility;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using CBS;
using CBS.Models;
using CBS.Scriptable;
using CBS.UI;
using CBS.Utils;
using strange.extensions.mediation.impl;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Login.Scripts.Views
{
    public class LoginFormView : ViewZeitnot
    {
        private AuthPrefabs AuthUIData { get; set; }

        [SerializeField] internal InputField MailInput;
        [SerializeField] internal InputField PasswordInput;
        [SerializeField] internal InputField CustomIDInput;
        [field: SerializeField] private ButtonZeitnot LoginWithMailButton { get; set; }
        [field: SerializeField] private ButtonZeitnot LoginWithDeviceIdButton { get; set; }
        [field: SerializeField] private ButtonZeitnot LoginWithCustomIdButton { get; set; }
        
        private Canvas Canvas { get; set; }

        internal Signal onLoginWithMailButtonClick = new Signal();
        internal Signal onLoginWithDeviceIDButtonClick = new Signal();
        internal Signal onLoginWithCustomIdButtonClick = new Signal();
        
        internal void Init()
        {
            LoginWithMailButton.onClick.AddListener(OnLoginWithMailButtonClick);
            LoginWithDeviceIdButton.onClick.AddListener(OnLoginWithDeviceIDButtonClick);
            LoginWithCustomIdButton.onClick.AddListener(OnLoginWithCustomIdButtonClick);

            Canvas = GetComponent<Canvas>();
        }

        private void OnDisable()
        {
            LoginWithMailButton.onClick.RemoveListener(OnLoginWithMailButtonClick);
            LoginWithDeviceIdButton.onClick.RemoveListener(OnLoginWithDeviceIDButtonClick);
            LoginWithCustomIdButton.onClick.RemoveListener(OnLoginWithCustomIdButtonClick);
            
            Clear();
        }

        private void OnLoginWithMailButtonClick()
        {
            onLoginWithMailButtonClick.Dispatch();
        }        
        private void OnLoginWithDeviceIDButtonClick()
        {
            onLoginWithDeviceIDButtonClick.Dispatch();
        }
        private void OnLoginWithCustomIdButtonClick()
        {
            onLoginWithCustomIdButtonClick.Dispatch();
        }
        
        public void OnRegistration()
        {
            var registrationPrefab = AuthUIData.RegisterForm;
            UIView.ShowWindow(registrationPrefab);
            HideWindow();
        }

        public void OnForgotPassword()
        {
            var recoveryPrefab = AuthUIData.RecoveryForm;
            UIView.ShowWindow(recoveryPrefab);
            HideWindow();
        }

        internal bool IsInputValid()
        {
            bool mailValid = !string.IsNullOrEmpty(MailInput.text);
            bool passwordValid = !string.IsNullOrEmpty(PasswordInput.text);
            return mailValid && passwordValid;
        }

        private void Clear()
        {
            MailInput.text = string.Empty;
            PasswordInput.text = string.Empty;
        }

        private void HideWindow()
        {
            gameObject.SetActive(false);
        }

        internal void ShowLoadingPopup()
        {
            new PopupViewer().ShowLoadingPopup();
        }              
        
        internal void HideLoadingPopup()
        {
            new PopupViewer().HideLoadingPopup();
        }        
        
        internal void ShowErrorPopup()
        {
            new PopupViewer().ShowSimplePopup(new PopupRequest
            {
                Title = AuthTXTHandler.ErrorTitle,
                Body = AuthTXTHandler.InvalidInput
            });
        }        
        internal void ShowPlayfabErrorPopup(CBSLoginResult result)
        {
            new PopupViewer().ShowFabError(result.Error);
        }
    }
}