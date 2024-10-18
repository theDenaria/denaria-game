using strange.extensions.command.impl;
using _Project.Matchmaking.Scripts.Services;
using _Project.Matchmaking.Scripts.Enums;

namespace _Project.Matchmaking.Scripts.Commands
{
    public class StartMatchmakingCommand : Command
    {
        [Inject] public IMatchmakingService MatchmakingService { get; set; }
        [Inject] public MatchmakingPlatformEnum Platform { get; set; }

        public override void Execute()
        {
            MatchmakingService.Init();
            MatchmakingService.StartMatchmaking(Platform, MatchmakingServiceQueueMode._1v1);
        }
    }
}