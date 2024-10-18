using _Project.NetworkManagement.TPSServer.Scripts.Services;
using _Project.NetworkManagement.TPSServer.Scripts.Signals;
using strange.extensions.command.impl;

namespace _Project.NetworkManagement.TPSServer.Scripts.Commands
{
    public class TPSServerConnectCommand : Command
    {
        [Inject] public ITPSServerService TPSServerService { get; set; }
        [Inject] public TPSServerConnectCommandData TPSServerConnectCommandData { get; set; }
        public override void Execute()
        {
            TPSServerService.Init(TPSServerConnectCommandData.SessionId, TPSServerConnectCommandData.ServerEndPoint, TPSServerConnectCommandData.ServerPort);
            TPSServerService.ConnectToTPSServer();
        }
    }
}