using _Project.Analytics.Core.Scripts.Models;
using _Project.Analytics.Core.Scripts.Services;
using strange.extensions.command.impl;

namespace _Project.Analytics.Core.Scripts.Commands
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