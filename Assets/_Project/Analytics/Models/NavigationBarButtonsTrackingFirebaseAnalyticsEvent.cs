namespace _Project.Analytics.Models
{
    public class NavigationBarButtonsTrackingFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
    {
        public NavigationBarButtonsTrackingFirebaseAnalyticsEvent(string button_type, string current_page)
        {
            EventName = "navigation_bar_buttons_tracking";
            
            EventParameters.Add(nameof(button_type), new FirebaseAnalyticsEventParameter(button_type));
            EventParameters.Add(nameof(current_page), new FirebaseAnalyticsEventParameter(current_page));

            EventParameters.Add("event_timestamp", new FirebaseAnalyticsEventParameter(DateUtility.GetCurrentEpochSeconds().ToString()));
        }
    }
}