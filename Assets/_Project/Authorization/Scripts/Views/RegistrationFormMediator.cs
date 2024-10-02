using _Project.Login.Controllers;
using _Project.Login.Scripts.Signals;
using CBS.Models;
using CBS.UI;
using strange.extensions.mediation.impl;

namespace _Project.Login.Scripts.Views
{
    public class RegistrationFormMediator : Mediator
    {
        [Inject] public RegistrationFormView View { get; set; }
        [Inject] public RegisterWithMailAndPasswordSignal RegisterWithMailAndPasswordSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.onSendReqistrationButtonClick.AddListener(HandleOnSendReqistrationButtonClick);
            View.onBackToLoginButtonClick.AddListener(HandleOnBackToLoginButtonClick);
            View.Init();
        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.onSendReqistrationButtonClick.AddListener(HandleOnSendReqistrationButtonClick);
            View.onBackToLoginButtonClick.AddListener(HandleOnBackToLoginButtonClick);
        }

        private void HandleOnSendReqistrationButtonClick()
        {
            bool passInput = View.IsInputValid();
            if (passInput)
            {
                string mail = View.MailInput.text;
                string password = View.PasswordInput.text;
                string displayName = View.DisplayNameInput.text;

                RegisterWithMailAndPasswordCommandData registerWithMailAndPasswordCommandData =
                    new RegisterWithMailAndPasswordCommandData(mail, password, displayName);
                RegisterWithMailAndPasswordSignal.Dispatch(registerWithMailAndPasswordCommandData);
            }
        }        
        
        private void HandleOnBackToLoginButtonClick()
        {
            View.ShowLoginForm();
        }        
        
        [ListensTo(typeof(RegistrationCompletedSignal))]
        public void OnRegistrationCompletedSignal(BaseAuthResult result)
        {
            new PopupViewer().HideLoadingPopup();
            if (result.IsSuccess)
            {
                View.ShowRegisterSucceededPopup();
            }
            else
            {
                View.ShowRegistrationFailedPopup(result);
            }
        }
        
    }
}