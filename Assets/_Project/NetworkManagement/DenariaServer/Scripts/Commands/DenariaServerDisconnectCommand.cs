using strange.extensions.command.impl;
using _Project.NetworkManagement.DenariaServer.Scripts.Services;

namespace _Project.NetworkManagement.DenariaServer.Scripts.Commands
{
    public class DenariaServerDisonnectCommand : Command
    {
        [Inject] public IDenariaServerService DenariaServerService { get; set; }

        public override void Execute()
        {
            DenariaServerService.DisconnectFromDenariaServer();
        }
    }
}