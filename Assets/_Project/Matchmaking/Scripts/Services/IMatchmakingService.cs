using _Project.Matchmaking.Scripts.Enums;

namespace _Project.Matchmaking.Scripts.Services
{
    public interface IMatchmakingService
    {
        public void Init();
        public void StartMatchmaking(MatchmakingPlatformEnum platform, MatchmakingServiceQueueMode queueMode);
        public void SendPlayerReady();
        public void CancelMatchmaking();
        public void Disconnect();
    }
}
