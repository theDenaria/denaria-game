using _Project.Login.Services;
using strange.extensions.command.impl;

namespace _Project.Login.Controllers
{
    public class BaseLoginCommand : Command
    {
        [Inject] public ILoginService LoginService { get; set; }

        public override void Execute()
        {
        }
    }
}