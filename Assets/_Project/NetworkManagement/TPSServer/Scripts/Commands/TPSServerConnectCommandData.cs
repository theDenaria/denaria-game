namespace _Project.NetworkManagement.TPSServer.Scripts.Commands
{
    public class TPSServerConnectCommandData
    {
        public string ServerEndPoint { get; set; }
        public ushort ServerPort { get; set; }

        public int SessionId { get; set; }

        public TPSServerConnectCommandData(int sessionId, string serverEndPoint, ushort serverPort)
        {
            ServerEndPoint = serverEndPoint;
            ServerPort = serverPort;
            SessionId = sessionId;
        }
    }
}