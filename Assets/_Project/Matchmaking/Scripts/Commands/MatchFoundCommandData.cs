using System.Collections.Generic;

namespace _Project.Matchmaking.Scripts.Commands
{
    public class MatchFoundCommandData
    {
        public int SessionId { get; set; }
        public List<MatchFoundInfo.Player> Players { get; set; }

        public MatchFoundCommandData(int sessionId, List<MatchFoundInfo.Player> players)
        {
            SessionId = sessionId;
            Players = players;
        }
    }
}