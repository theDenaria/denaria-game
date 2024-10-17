using System.Collections.Generic;
using _Project.Matchmaking.Scripts.Enums;

namespace _Project.Matchmaking.Scripts.Commands
{
    public class ConnectMatchCommandData
    {
        public MatchmakingPlatformEnum Platform { get; set; }
        public int SessionId { get; set; }
        public string ServerEndPoint { get; set; }
        public ushort ServerPort { get; set; }

        public ConnectMatchCommandData(MatchStartInfo matchStartInfo, MatchmakingPlatformEnum platform)
        {
            Platform = platform;
            SessionId = matchStartInfo.session_id;
            ServerEndPoint = matchStartInfo.tps_server_address;
            ServerPort = matchStartInfo.tps_server_port;
        }
    }
}