using _Project.Analytics.FirebaseAnalytics.Scripts.Models;

namespace _Project.Analytics.CustomEvents.Scripts.Models
{
    public class AdultContentPopupFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
    {
        public AdultContentPopupFirebaseAnalyticsEvent(string request)
        {
            EventName = "adult_content_popup";
            
            EventParameters.Add(nameof(request), new FirebaseAnalyticsEventParameter(request));
        }
    }
}