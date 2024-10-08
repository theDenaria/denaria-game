namespace _Project.NetworkManagement.Scripts.Controllers
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