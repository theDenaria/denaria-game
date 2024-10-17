using _Project.NetworkManagement.DenariaServer.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.NetworkManagement.DenariaServer.Scripts.Commands
{
    public class DenariaServerConnectCommand : Command
    {
        [Inject] public IDenariaServerService DenariaServerService { get; set; }

        public override void Execute()
        {
            DenariaServerService.Init();
            DenariaServerService.ConnectToDenariaServer();
        }
    }
}