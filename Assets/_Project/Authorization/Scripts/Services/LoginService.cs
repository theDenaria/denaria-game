using _Project.Authorization.Scripts.Commands;
using _Project.Authorization.Scripts.Signals;
using _Project.SceneManagementUtilities.Scripts.Signals;
using CBS;
using CBS.Models;

namespace _Project.Authorization.Scripts.Services
{
    public class LoginService : ILoginService
    {
        private IAuth CBSAuth { get; set; }
        [Inject] public UserLoginCompletedSignal UserLoginCompletedSignal { private get; set; }
        [Inject] public ChangeSceneGroupSignal ChangeSceneGroupSignal { private get; set; }

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
            UserLoginCompletedSignal.Dispatch(result);
        }
    }
}