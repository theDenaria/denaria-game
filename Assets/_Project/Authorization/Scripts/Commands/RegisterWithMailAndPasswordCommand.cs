using _Project.Authorization.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.Authorization.Scripts.Commands
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