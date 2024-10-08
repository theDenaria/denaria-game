using _Project.Authorization.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.Authorization.Scripts.Commands
{
    public class BaseLoginCommand : Command
    {
        [Inject] public ILoginService LoginService { get; set; }

        public override void Execute()
        {
        }
    }
}