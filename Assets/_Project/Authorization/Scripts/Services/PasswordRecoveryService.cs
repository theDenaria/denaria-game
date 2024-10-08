using _Project.Authorization.Scripts.Commands;
using CBS;

namespace _Project.Authorization.Scripts.Services
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