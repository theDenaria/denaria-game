

namespace _Project.Analytics.Models
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