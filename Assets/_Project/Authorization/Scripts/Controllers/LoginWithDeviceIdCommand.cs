using strange.extensions.command.impl;

namespace _Project.Login.Controllers
{
    public class LoginWithDeviceIdCommand : BaseLoginCommand
    {
        [Inject] public LoginWithMailAndPasswordCommandData LoginWithMailAndPasswordCommandData { get; set; }

        public override void Execute()
        {
            
        }
    }
}