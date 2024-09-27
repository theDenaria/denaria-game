using _Project.Login.Controllers;
using _Project.Login.Scripts.Signals;
using CBS;
using CBS.Models;

namespace _Project.Login.Services
{
    public class LoginService : ILoginService
    {
        private IAuth CBSAuth { get; set; }
        [Inject] public UserLoginCompletedSignal OnLoginResponseReceivedSignal { private get; set; }

        public void LoginWithMailAndPassword(LoginWithMailAndPasswordCommandData loginWithMailAndPasswordCommandData)
        {
            CBSAuth = CBSAuthModule.Get<CBSAuthModule>();
            var loginRequest = new CBSMailLoginRequest 
            {
                Mail = loginWithMailAndPasswordCommandData.Mail,
                Password = loginWithMailAndPasswordCommandData.Password
            };
            CBSAuth.LoginWithMailAndPassword(loginRequest, OnLoginResponseReceived);
        }

        public void LoginWithDeviceId(LoginWithDeviceIdCommandData loginWithDeviceIdCommandData)
        {
            CBSAuth.LoginWithDevice(OnLoginResponseReceived);
        }

        public void LoginWithCustomId(LoginWithCustomIdCommandData loginWithMailAndPasswordCommandData)
        {
            CBSAuth.LoginWithCustomID(loginWithMailAndPasswordCommandData.CustomId, OnLoginResponseReceived);
        }

        private void OnLoginResponseReceived(CBSLoginResult result)
        {
            if (result.IsSuccess)
            {
                var isNew = result.IsNew;
                var profileID = result.ProfileID;
                var playfabLoginResult = result.Result;
            }
            else
            {
                Debug.Log(result.Error.Message);
            }

            OnLoginResponseReceivedSignal.Dispatch(result);
        }
    }
}