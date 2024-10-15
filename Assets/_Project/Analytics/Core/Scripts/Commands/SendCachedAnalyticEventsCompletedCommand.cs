using _Project.Utilities;
using strange.extensions.command.impl;

namespace _Project.Analytics.Core.Scripts.Commands
{
    public class SendCachedAnalyticEventsCompletedCommand : Command
    {
        public override void Execute()
        {
            Constants.CACHED_EVENTS_SENT = true;
        } 
    }
}