using _Project.Login.Controllers;

namespace _Project.Login.Services
{
    public interface IPasswordRecoveryService
    {
        public void SendPasswordRecovery(RequestPasswordRecoveryCommandData requestPasswordRecoveryCommandData);
    }
}