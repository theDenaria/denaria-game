
using _Project.Matchmaking.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.Matchmaking.Scripts.Commands
{
    public class MatchmakingDisconnectCommand : Command
    {
        [Inject] public IMatchmakingService MatchmakingService { get; set; }

        public override void Execute()
        {
            MatchmakingService.Disconnect();
        }
    }
}