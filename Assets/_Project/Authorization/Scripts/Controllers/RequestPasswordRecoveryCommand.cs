using _Project.Login.Services;
using strange.extensions.command.impl;

namespace _Project.Login.Controllers
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