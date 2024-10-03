
using Unity.Networking.Transport;

namespace _Project.NetworkManagement.Scripts.Models
{
    public class ServerConnection
    {
        public NetworkConnection NetworkConnection { get; set; }
        public NetworkPipeline ReliablePipeline { get; set; }
        public bool IsConnectionAccepted { get; set; }

        public ServerConnection(NetworkPipeline reliablePipeline)
        {
            ReliablePipeline = reliablePipeline;
            NetworkConnection = default;
            IsConnectionAccepted = false;
        }
    }
}
