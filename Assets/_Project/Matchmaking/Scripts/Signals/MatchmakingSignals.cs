using _Project.Matchmaking.Scripts.Commands;
using strange.extensions.signal.impl;
namespace _Project.Matchmaking.Scripts.Signals
{
    public class StartMatchmakingSignal : Signal { }
    public class CancelMatchmakingSignal : Signal { }

    public class QueueStartedSignal : Signal { }
    public class MatchFoundSignal : Signal<MatchFoundCommandData> { }
    public class QueueFinishedSignal : Signal { }

}