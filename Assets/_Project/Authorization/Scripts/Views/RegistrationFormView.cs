using _Project.StrangeIOCUtility.Scripts.Views;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using CBS.Models;
using CBS.UI;
using CBS.Utils;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Authorization.Scripts.Views
{
    public class RegistrationFormView : ViewZeitnot
    {
        [field: SerializeField] private GameObject LoginForm { get; set; }
        [field: SerializeField] private GameObject ForgotPasswordForm { get; set; }
        [field: SerializeField] private ButtonZeitnot SendReqistrationButton { get; set; }
        [field: SerializeField] private ButtonZeitnot BackToLoginButton { get; set; }

        [SerializeField] internal InputField MailInput;
        [SerializeField] internal InputField PasswordInput;
        [SerializeField] internal InputField PasswordRepeatInput;
        [SerializeField] internal InputField DisplayNameInput;
        
        internal Signal onSendReqistrationButtonClick = new Signal();
        internal Signal onBackToLoginButtonClick = new Signal();
        
        private void OnEnable()
        {
            Init();
        }
        
        private void OnDisable()
        {
            SendReqistrationButton.onClick.RemoveListener(OnSendReqistrationButtonClick);
            BackToLoginButton.onClick.RemoveListener(OnBackToLoginButtonClick);
            Clear();
        }
        
        internal void Init()
        {
            SendReqistrationButton.onClick.AddListener(OnSendReqistrationButtonClick);
            BackToLoginButton.onClick.AddListener(OnBackToLoginButtonClick);
        }
        
        private void Clear()
        {
            MailInput.text = string.Empty;
            PasswordInput.text = string.Empty;
            PasswordRepeatInput.text = string.Empty;
            DisplayNameInput.text = string.Empty;
        }

        public void OnBackToLoginButtonClick()
        {
            onBackToLoginButtonClick.Dispatch();
        }
        
        public void ShowLoginForm()
        {
            LoginForm.gameObject.SetActive(true);
            HideWindow();
        }
        
        public void OnSendReqistrationButtonClick()
        {
            onSendReqistrationButtonClick.Dispatch();
            
        }
        
        internal bool IsInputValid()
        {
            bool mailValid = !string.IsNullOrEmpty(MailInput.text);
            bool passwordValid = !string.IsNullOrEmpty(PasswordInput.text);
            bool passwordRepeatValid = !string.IsNullOrEmpty(PasswordRepeatInput.text);
            bool nameValid = !string.IsNullOrEmpty(DisplayNameInput.text);

            bool fieldsValid = mailValid & passwordValid & passwordRepeatValid & nameValid;
            if (!fieldsValid)
            {
                ShowInvalidInputPopup();
                return false;
            }

            bool passwordSame = PasswordInput.text == PasswordRepeatInput.text;
            if (!passwordSame)
            {
                ShowInvalidPasswordPopup();
                return false;
            }

            return true;
        }
        
        internal void ShowRegistrationFailedPopup(CBSBaseResult result)
        {
            new PopupViewer().ShowSimplePopup(new PopupRequest
            {
                Title = AuthTXTHandler.ErrorTitle,
                Body = result.Error.Message
            });
        }         
        
        internal void ShowRegisterSucceededPopup()
        {
            new PopupViewer().ShowSimplePopup(new PopupRequest
            {
                Title = AuthTXTHandler.RegisterComplete,
                Body = AuthTXTHandler.RegistrationMessage,
                OnOkAction = ShowLoginForm
            });
        } 
        
        internal void ShowInvalidInputPopup()
        {
            new PopupViewer().ShowSimplePopup(new PopupRequest
            {
                Title = AuthTXTHandler.ErrorTitle,
                Body = AuthTXTHandler.InvalidInput
            });
        }        
        internal void ShowInvalidPasswordPopup()
        {
            new PopupViewer().ShowSimplePopup(new PopupRequest
            {
                Title = AuthTXTHandler.ErrorTitle,
                Body = AuthTXTHandler.InvalidPassword
            });
        } 

        private void HideWindow()
        {
            gameObject.SetActive(false);
        }
    }
}