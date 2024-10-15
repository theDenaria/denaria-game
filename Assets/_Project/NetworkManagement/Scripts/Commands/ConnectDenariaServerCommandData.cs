namespace _Project.NetworkManagement.Scripts.Commands
{
    public class ConnectDenariaServerCommandData
    {
        public string PlayerId { get; set; }

        public string SessionTicket { get; set; }

        public ConnectDenariaServerCommandData(string playerId, string sessionTicket)
        {
            PlayerId = playerId;
            SessionTicket = sessionTicket;
        }
    }
}