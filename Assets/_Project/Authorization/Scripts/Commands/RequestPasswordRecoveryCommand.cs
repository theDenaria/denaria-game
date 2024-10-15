using _Project.Authorization.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.Authorization.Scripts.Commands
{
    public class RequestPasswordRecoveryCommand : Command
    {
        [Inject] public IPasswordRecoveryService PasswordRecoveryService { get; set; }
        [Inject] public RequestPasswordRecoveryCommandData RequestPasswordRecoveryCommandData { get; set; }

        public override void Execute()
        {
            PasswordRecoveryService.SendPasswordRecovery(RequestPasswordRecoveryCommandData);
        }
    }
}