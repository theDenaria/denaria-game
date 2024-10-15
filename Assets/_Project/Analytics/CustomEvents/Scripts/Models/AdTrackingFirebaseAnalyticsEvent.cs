using _Project.Analytics.FirebaseAnalytics.Scripts.Models;

namespace _Project.Analytics.CustomEvents.Scripts.Models
{
    public class AdTrackingFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
    {
        public AdTrackingFirebaseAnalyticsEvent(string ad_source_screen_id, string ad_source_type, string ad_type, string result)
        {
            EventName = "ad_tracking";
            
            EventParameters.Add("screen_source", new FirebaseAnalyticsEventParameter(ad_source_screen_id));
            EventParameters.Add(nameof(ad_source_type), new FirebaseAnalyticsEventParameter(ad_source_type));
            EventParameters.Add(nameof(ad_type), new FirebaseAnalyticsEventParameter(ad_type));
            EventParameters.Add(nameof(result), new FirebaseAnalyticsEventParameter(result));

        }
    }
}