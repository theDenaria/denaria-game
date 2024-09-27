using _Project.Login.Controllers;
using CBS;
using CBS.Models;

namespace _Project.Login.Services
{
    public class LoginService : ILoginService
    {
        private IAuth CBSAuth { get; set; }

        public void LoginWithMailAndPassword(LoginWithMailAndPasswordCommandData loginWithMailAndPasswordCommandData)
        {
            CBSAuth = CBSAuthModule.Get<CBSAuthModule>();
            var loginRequest = new CBSMailLoginRequest 
            {
                Mail = loginWithMailAndPasswordCommandData.Mail,
                Password = loginWithMailAndPasswordCommandData.Password
            };
            CBSAuth.LoginWithMailAndPassword(loginRequest, OnLogin);
        }
        
        private void OnLogin(CBSLoginResult result)
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
        }
    }
}