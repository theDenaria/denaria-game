using Unity.Networking.Transport;

namespace _Project.NetworkManagement.Scripts.Models
{
    public interface INetworkManagerModel
    {
        public string DenariaServerAddress { get; set; }
        public string DenariaServerPort { get; set; }

        public string MatchmakingServerAddress { get; set; }
        public string MatchmakingServerPort { get; set; }

        public NetworkDriver NetworkDriver { get; set; }
        public ServerConnection DenariaServerConnection { get; set; }
        // public ServerConnection MatchmakingServerConnection { get; set; }

        public string PlayerId { get; set; }

        // public NetworkPipeline ReliablePipeline { get; set; }
        // public NetworkPipeline UnreliablePipeline { get; set; }

        public void ConnectToDenariaServer();
        public void DisconnectFromDenariaServer();
    }
}