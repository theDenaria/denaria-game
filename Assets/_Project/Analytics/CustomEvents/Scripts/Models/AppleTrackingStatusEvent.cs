

using _Project.Analytics.FirebaseAnalytics.Scripts.Models;

namespace _Project.Analytics.CustomEvents.Scripts.Models
{
    public class AppleTrackingStatusEvent : FirebaseAnalyticsEvent
    {
        public AppleTrackingStatusEvent(string request)
        {
            EventName = "AppTrackingAuthorizationStatus";
            
            EventParameters.Add(nameof(request), new FirebaseAnalyticsEventParameter(request));
        }
    }
}