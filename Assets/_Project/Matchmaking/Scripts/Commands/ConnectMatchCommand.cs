
using _Project.Matchmaking.Scripts.Enums;
using _Project.Matchmaking.Scripts.Services;
using _Project.NetworkManagement.TPSServer.Scripts.Commands;
using _Project.NetworkManagement.TPSServer.Scripts.Services;
using _Project.NetworkManagement.TPSServer.Scripts.Signals;
using strange.extensions.command.impl;

namespace _Project.Matchmaking.Scripts.Commands
{
    public class ConnectMatchCommand : Command
    {
        [Inject] public ConnectMatchCommandData ConnectMatchCommandData { get; set; }
        [Inject] public TPSServerConnectSignal TPSServerConnectSignal { get; set; }
        public override void Execute()
        {
            switch (ConnectMatchCommandData.Platform)
            {
                case MatchmakingPlatformEnum.None:
                    break;
                case MatchmakingPlatformEnum.ThirdPersonShooter:
                    TPSServerConnectSignal.Dispatch(new TPSServerConnectCommandData(ConnectMatchCommandData.SessionId, ConnectMatchCommandData.ServerEndPoint, ConnectMatchCommandData.ServerPort));
                    break;
                case MatchmakingPlatformEnum.Platformer:
                    break;
            }

        }
    }
}