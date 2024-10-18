
using _Project.Matchmaking.Scripts.Enums;

namespace _Project.Matchmaking.Scripts.Signals
{
    public class PlatformTriggerEnterSignalData
    {
        public MatchmakingPlatformEnum Platform { get; set; }

        public PlatformTriggerEnterSignalData(MatchmakingPlatformEnum platform)
        {
            Platform = platform;
        }
    }
}