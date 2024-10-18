namespace _Project.PlayerSessionInfo.Scripts.Models
{
    public interface IPlayerSessionInfoModel
    {
        public string PlayerId { get; set; }
        public string SessionTicket { get; set; }
        public void Init(string playerId, string sessionTicket);
    }
}
