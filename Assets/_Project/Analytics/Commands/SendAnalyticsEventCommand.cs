using _Project.Analytics.Models;
using _Project.Analytics.Services;
using strange.extensions.command.impl;

namespace _Project.Analytics.Commands
{
    public class SendAnalyticsEventCommand : Command
    {
        [Inject] public IAnalyticsEvent AnalyticsEvent { get; set; }
        [Inject] public IAnalyticsService AnalyticsService { get; set; }

        public override void Execute()
        {
            AnalyticsService.SendEvent(AnalyticsEvent);
        }
    }
}