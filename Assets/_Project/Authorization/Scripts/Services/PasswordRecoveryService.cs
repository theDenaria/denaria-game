using _Project.Login.Controllers;
using CBS;

namespace _Project.Login.Services
{
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        private IAuth CBSAuth { get; set; }

        public void SendPasswordRecovery(RequestPasswordRecoveryCommandData requestPasswordRecoveryCommandData)
        {
            CBSAuth.SendPasswordRecovery(requestPasswordRecoveryCommandData.Mail, requestPasswordRecoveryCommandData.OnRecoverySent);
        }

    }
}