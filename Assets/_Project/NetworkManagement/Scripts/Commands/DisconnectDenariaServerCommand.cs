using _Project.NetworkManagement.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.NetworkManagement.Scripts.Commands
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