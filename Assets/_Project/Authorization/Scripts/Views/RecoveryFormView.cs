using _Project.StrangeIOCUtility;
using _Project.UIZeitnot.ButtonZeitnot.Scripts;
using CBS;
using CBS.Models;
using CBS.Scriptable;
using CBS.UI;
using CBS.Utils;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Login.Scripts.Views
{
    public class RecoveryFormView : ViewZeitnot
    {
        [SerializeField] internal InputField EmailField { get; set; }
        [field: SerializeField] private GameObject LoginForm { get; set; }
        [field: SerializeField] private ButtonZeitnot RecoverButton { get; set; }
        [field: SerializeField] private ButtonZeitnot GoBackButton { get; set; }

        internal Signal onRecoverButtonClick = new Signal();
        internal Signal onGoBackButtonClick = new Signal();

        internal void Init() //TODO: AddListeners On Enable
        {
            RecoverButton.onClick.AddListener(SendRecovery);
            GoBackButton.onClick.AddListener(HandleReturnToLogin);
        }
        
        private void OnEnable()
        {
            Init();
        }

        private void OnDisable()
        {
            RecoverButton.onClick.RemoveListener(SendRecovery);
            GoBackButton.onClick.RemoveListener(HandleReturnToLogin);
            
            Clear();
        }

        private void Clear()
        {
            EmailField.text = string.Empty;
        }

        public void SendRecovery()
        {
            onRecoverButtonClick.Dispatch();
        }
        
        public void OnRecoverySent(CBSBaseResult result)
        {
            if (result.IsSuccess)
            {
                string email = EmailField.text;
                ShowRecoverySucceededPopup(email);
            }
            else
            {
                ShowRecoveryFailedPopup(result);
            }
        }
        
        public void HandleReturnToLogin()
        {
            onGoBackButtonClick.Dispatch();
        }
        
        public void ReturnToLogin()
        {
            LoginForm.gameObject.SetActive(true);
            HideWindow();
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
        
        internal void ShowRecoveryFailedPopup(CBSBaseResult result)
        {
            new PopupViewer().ShowSimplePopup(new PopupRequest
            {
                Title = AuthTXTHandler.ErrorTitle,
                Body = result.Error.Message
            });
        } 
        
        internal void ShowRecoverySucceededPopup(string email)
        {
            new PopupViewer().ShowSimplePopup(new PopupRequest
            {
                Title = AuthTXTHandler.SuccessTitle,
                Body = AuthTXTHandler.GetRecoveryMessage(email),
                OnOkAction = Clear
            });
        } 
        
    }
}
