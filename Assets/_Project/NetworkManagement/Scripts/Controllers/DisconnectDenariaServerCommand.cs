using strange.extensions.command.impl;
using _Project.NetworkManagement.Scripts.Services;

namespace _Project.NetworkManagement.Scripts.Controllers
{
    public class DisonnectDenariaServerCommand : Command
    {
        [Inject] public IDenariaServerService DenariaServerService { get; set; }

        public override void Execute()
        {
            DenariaServerService.DisconnectFromDenariaServer();
        }
    }
}