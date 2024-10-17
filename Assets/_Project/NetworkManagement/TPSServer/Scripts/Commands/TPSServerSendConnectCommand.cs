using strange.extensions.command.impl;
using _Project.NetworkManagement.TPSServer.Scripts.Services;

namespace _Project.NetworkManagement.TPSServer.Scripts.Commands
{
    public class TPSServerSendConnectCommand : Command
    {
        [Inject] public ITPSServerService TPSServerService { get; set; }

        public override void Execute()
        {
            TPSServerService.StartSendingConnectMessage();
        }
    }
}