using _Project.Login.Controllers;
using _Project.Login.Scripts.Signals;
using strange.extensions.mediation.impl;

namespace _Project.Login.Scripts.Views
{
    public class RecoveryFormMediator : Mediator
    {
        [Inject] public RecoveryFormView View { get; set; }
        [Inject] public RequestPasswordRecoverySignal RequestPasswordRecoverySignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.onRecoverButtonClick.AddListener(HandleOnRecoverButtonClicked);
            View.onGoBackButtonClick.AddListener(HandleOnGoBackButtonClicked);
            View.Init();
        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.onRecoverButtonClick.RemoveListener(HandleOnRecoverButtonClicked);
            View.onGoBackButtonClick.RemoveListener(HandleOnGoBackButtonClicked);
        }

        private void HandleOnRecoverButtonClicked()
        {
            string email = View.EmailField.text;
            if (string.IsNullOrEmpty(email))
            {
                View.ShowErrorPopup();
            }

            RequestPasswordRecoveryCommandData requestPasswordRecoveryCommandData =
                new RequestPasswordRecoveryCommandData(email, View.OnRecoverySent);
            RequestPasswordRecoverySignal.Dispatch(requestPasswordRecoveryCommandData);
        }
        
        private void HandleOnGoBackButtonClicked()
        {
            View.ReturnToLogin();
            //Auth.LoginWithDevice(OnUserLogined);
        }  
    }
}