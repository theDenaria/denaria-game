namespace _Project.NetworkManagement.Scripts.Commands
{
    public class ConnectDenariaServerCommandData
    {
        public string PlayerId { get; set; }

        public ConnectDenariaServerCommandData(string playerId)
        {
            PlayerId = playerId;
        }
    }
}