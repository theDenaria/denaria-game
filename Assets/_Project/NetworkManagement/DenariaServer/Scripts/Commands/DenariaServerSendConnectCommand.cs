using strange.extensions.command.impl;
using _Project.NetworkManagement.DenariaServer.Scripts.Services;

namespace _Project.NetworkManagement.DenariaServer.Scripts.Commands
{
    public class DenariaServerSendConnectCommand : Command
    {
        [Inject] public IDenariaServerService DenariaServerService { get; set; }

        public override void Execute()
        {
            DenariaServerService.StartSendingConnectMessage();
        }
    }
}