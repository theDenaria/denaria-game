using System.Collections.Generic;
using _Project.Analytics.Core.Scripts.Enums;
using _Project.Analytics.Core.Scripts.Models;
using _Project.Analytics.Core.Scripts.Services;
using _Project.Analytics.UnityAnalytics.Scripts.Models;
using _Project.LoggingAndDebugging;
using Unity.Services.Analytics;


namespace _Project.Analytics.UnityAnalytics.Scripts.Services
{
    public class UnityAnalyticsService : BaseAnalyticsService<OldUnityAnalyticsEvent,OldUnityAnalyticsEventParameter>
    {
        public override void ValidateAnalyticsEventType(IAnalyticsEvent analyticsEvent)
        {
            if (analyticsEvent is not Unity.Services.Analytics.Event)
            { 
                DebugLoggerMuteable.Log("Please provide Unity.Services.Analytics.Event");
                return;
            }
        }
        
        public override AnalyticsResultWrapper SendEventViaAPI(IAnalyticsEvent analyticsEvent)
        {
            Event unityAnalyticsEvent = (Event) analyticsEvent;

            AnalyticsService.Instance.RecordEvent(unityAnalyticsEvent);
            
            AnalyticsResultWrapper analyticsResultWrapper = new AnalyticsResultWrapper();
            
            return analyticsResultWrapper;
            
        }

        public override void LogSentAnalyticsEvent(BaseAnalyticsEvent<IAnalyticsEventParameter> unityAnalyticsEvent, AnalyticsResultWrapper analyticsResultWrapper)
        {
            string resultMessage = "AnalyticsResult of the event " + unityAnalyticsEvent.GetType().Name +
                                   unityAnalyticsEvent.EventName + ": " + analyticsResultWrapper;

            foreach (KeyValuePair<string, IAnalyticsEventParameter> unityAnalyticsEventParameter in unityAnalyticsEvent
                         .EventParameters)
            {
                resultMessage += $"\n{unityAnalyticsEventParameter.Key}: {unityAnalyticsEventParameter.Value}";
            }

            DebugLoggerMuteable.Log(resultMessage);
        }
    }
}