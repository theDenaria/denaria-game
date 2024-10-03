using Unity.Networking.Transport;

namespace _Project.NetworkManagement.Scripts.Models
{
    public class NetworkManagerModel : INetworkManagerModel
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

        public NetworkManagerModel()
        {
            NetworkDriver = NetworkDriver.Create();
            DenariaServerAddress = "127.0.0.1";
            DenariaServerPort = "5000";
            MatchmakingServerAddress = "127.0.0.1";
            MatchmakingServerPort = "5001";

            PlayerId = null;

            DenariaServerConnection = new ServerConnection(NetworkDriver.CreatePipeline(typeof(ReliableSequencedPipelineStage)));
        }

        public void ConnectToDenariaServer()
        {
            var endpoint = NetworkEndpoint.Parse(DenariaServerAddress, ushort.Parse(DenariaServerPort));
            DenariaServerConnection.NetworkConnection = NetworkDriver.Connect(endpoint);
            DenariaServerConnection.IsConnectionAccepted = DenariaServerConnection.NetworkConnection.IsCreated;
        }

        public void DisconnectFromDenariaServer()
        {
            DenariaServerConnection.NetworkConnection.Disconnect(NetworkDriver);
            DenariaServerConnection.IsConnectionAccepted = false;
        }
    }
}