using _Project.Analytics.Core.Scripts.Signals;
using strange.extensions.command.impl;

namespace _Project.Analytics.Core.Scripts.Commands
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