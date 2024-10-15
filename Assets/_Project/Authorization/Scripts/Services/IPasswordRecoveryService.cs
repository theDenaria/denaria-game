using _Project.Authorization.Scripts.Commands;

namespace _Project.Authorization.Scripts.Services
{
    public interface IPasswordRecoveryService
    {
        public void SendPasswordRecovery(RequestPasswordRecoveryCommandData requestPasswordRecoveryCommandData);
    }
}