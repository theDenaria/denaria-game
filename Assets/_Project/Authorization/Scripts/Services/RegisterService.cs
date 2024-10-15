using _Project.Authorization.Scripts.Commands;
using _Project.Authorization.Scripts.Signals;
using CBS;
using CBS.Models;

namespace _Project.Authorization.Scripts.Services
{
    public class RegisterService : IRegisterService
    {
        private IAuth CBSAuth { get; set; }
        [Inject] public RegistrationCompletedSignal RegistrationCompletedSignal { get; set; }

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
            RegistrationCompletedSignal.Dispatch(result);
        }
    }
}