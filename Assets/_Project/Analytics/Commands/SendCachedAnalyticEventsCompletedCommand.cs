using strange.extensions.command.impl;
using _Project.Utilities;

namespace _Project.Analytics.Commands
{
    public class SendCachedAnalyticEventsCompletedCommand : Command
    {
        public override void Execute()
        {
            Constants.CACHED_EVENTS_SENT = true;
        } 
    }
}