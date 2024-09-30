using _Project.Login.Controllers;
using CBS;
using CBS.Models;

namespace _Project.Login.Services
{
    public class RegisterService : IRegisterService
    {
        private IAuth CBSAuth { get; set; }

        public void RegisterWithMailAndPassword(RegisterWithMailAndPasswordCommandData registerWithMailAndPasswordCommandData)
        {
            CBSAuth = CBSAuthModule.Get<CBSAuthModule>();

            var registerRequest = new CBSMailRegistrationRequest
            {
                Mail = registerWithMailAndPasswordCommandData.Mail,
                Password = registerWithMailAndPasswordCommandData.Password,
                DisplayName = registerWithMailAndPasswordCommandData.DisplayName
            };

            CBSAuth.RegisterWithMailAndPassword(registerRequest, OnRegister);
        }

        private void OnRegister(BaseAuthResult result)
        {
            if (result.IsSuccess)
            {
                var profileID = result.ProfileID;
            }
            else
            {
                Debug.Log(result.Error.Message);
            }
        }
    }
}