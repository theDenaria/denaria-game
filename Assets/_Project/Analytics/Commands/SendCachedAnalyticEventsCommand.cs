using _Project.Analytics.Signals;
using strange.extensions.command.impl;

namespace _Project.Analytics.Commands
{
    public class SendCachedAnalyticEventsCommand : Command
    {
        [Inject] public SendCachedAnalyticEventsSignal SendCachedAnalyticEventsSignal { get; set; }
        public override void Execute()
        {
            SendCachedAnalyticEventsSignal.Dispatch();
        }
    }
}