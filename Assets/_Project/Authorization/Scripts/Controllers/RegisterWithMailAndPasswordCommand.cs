using _Project.Login.Services;
using CBS.Models;
using CBS.UI;
using strange.extensions.command.impl;

namespace _Project.Login.Controllers
{
    public class RegisterWithMailAndPasswordCommand : Command
    {
        [Inject] public IRegisterService RegisterService { get; set; }
        [Inject] public RegisterWithMailAndPasswordCommandData RegisterWithMailAndPasswordCommandData { get; set; }

        public override void Execute()
        {
            RegisterService.RegisterWithMailAndPassword(RegisterWithMailAndPasswordCommandData);
        }
    }
}