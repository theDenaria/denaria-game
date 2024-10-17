namespace _Project.PlayerSessionInfo.Scripts.Models
{
    public class PlayerSessionInfoModel : IPlayerSessionInfoModel
    {
        public string PlayerId { get; set; }
        public string SessionTicket { get; set; }

        public void Init(string playerId, string sessionTicket)
        {
            if (string.IsNullOrEmpty(playerId) || string.IsNullOrEmpty(sessionTicket))
            {
                return;
            }
            if (playerId.Length > 16)
            {
                return;
            }
            PlayerId = playerId;
            SessionTicket = sessionTicket;
        }
    }
}