
using _Project.Matchmaking.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.Matchmaking.Scripts.Commands
{
    public class MatchFoundCommand : Command
    {
        [Inject] public MatchFoundCommandData MatchFoundCommandData { get; set; }
        // [Inject] public IMatchmakingService MatchmakingService { get; set; }

        public override void Execute()
        {
            Debug.Log($"MatchFoundCommand");
        }
    }
}