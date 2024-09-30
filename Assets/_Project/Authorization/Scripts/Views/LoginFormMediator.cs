using _Project.ForceUpdate.Scripts.Signals;
using _Project.Login.Controllers;
using _Project.Login.Scripts.Signals;
using CBS.Models;
using CBS.UI;
using CBS.Utils;
using strange.extensions.mediation.impl;

namespace _Project.Login.Scripts.Views
{
    public class LoginFormMediator : Mediator
    {
        [Inject] public LoginFormView View { get; set; }
        [Inject] public LoginWithMailAndPasswordSignal LoginWithMailAndPasswordSignal { get; set; }

        public override void OnRegister()
        {
            base.OnRegister();
            View.onLoginWithMailButtonClick.AddListener(HandleOnLoginWithMailButtonClicked);
            View.onLoginWithDeviceIDButtonClick.AddListener(HandleOnLoginWithDeviceIdButtonClicked);
            View.onLoginWithCustomIdButtonClick.AddListener(HandleOnLoginWithCustomIdButtonClicked);
            // View.Init();
        }

        public override void OnRemove()
        {
            base.OnRemove();
            View.onLoginWithMailButtonClick.RemoveListener(HandleOnLoginWithMailButtonClicked);
            View.onLoginWithDeviceIDButtonClick.RemoveListener(HandleOnLoginWithDeviceIdButtonClicked);
            View.onLoginWithCustomIdButtonClick.RemoveListener(HandleOnLoginWithCustomIdButtonClicked);
        }

        private void HandleOnLoginWithMailButtonClicked()
        {
            if (View.IsInputValid())
            {
                LoginWithMailAndPasswordCommandData loginWithMailAndPasswordCommandData =
                    new LoginWithMailAndPasswordCommandData(View.MailInput.text, View.PasswordInput.text);

                View.ShowLoadingPopup();

                LoginWithMailAndPasswordSignal.Dispatch(loginWithMailAndPasswordCommandData);
            }
            else
            {
                View.ShowErrorPopup();
            }
        }
        private void HandleOnLoginWithDeviceIdButtonClicked()
        {
            View.ShowLoadingPopup();
            //Auth.LoginWithDevice(OnUserLogined);
        }
        private void HandleOnLoginWithCustomIdButtonClicked()
        {
            //LoginWithMailAndPasswordSignal.Dispatch();

            string customID = View.CustomIDInput.text;
            if (!string.IsNullOrEmpty(customID))
            {
                View.ShowLoadingPopup();
                //Auth.LoginWithCustomID(customID, OnUserLogined);
            }
        }

        [ListensTo(typeof(UserLoginCompletedSignal))]
        public void OnUserLoginCompleted(CBSLoginResult result)
        {
            View.HideLoadingPopup();
            if (result.IsSuccess)
            {
                // goto main screen
                gameObject.SetActive(false);
            }
            else
            {
                // show error message
                View.ShowPlayfabErrorPopup(result);
            }
        }

    }
}