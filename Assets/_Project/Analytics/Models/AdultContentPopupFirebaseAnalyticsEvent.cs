namespace _Project.Analytics.Models
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