namespace _Project.Analytics.Models
{
    public class ScreenTrackingFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
    {
        public ScreenTrackingFirebaseAnalyticsEvent(string previous_screen_id, string current_screen_id, int screen_duration_inseconds, string request, string result)
        {
            EventName = "screen_tracking";
            EventParameters.Add(nameof(previous_screen_id),new FirebaseAnalyticsEventParameter(previous_screen_id));
            EventParameters.Add(nameof(current_screen_id),new FirebaseAnalyticsEventParameter(current_screen_id));
            EventParameters.Add(nameof(screen_duration_inseconds),new FirebaseAnalyticsEventParameter(screen_duration_inseconds));
            EventParameters.Add(nameof(request),new FirebaseAnalyticsEventParameter(request));
            EventParameters.Add(nameof(result),new FirebaseAnalyticsEventParameter(result));

            EventParameters.Add("event_timestamp", new FirebaseAnalyticsEventParameter(DateUtility.GetCurrentEpochSeconds().ToString()));
        }
        public ScreenTrackingFirebaseAnalyticsEvent(string previous_screen_id, string current_screen_id, int screen_duration_inseconds,
            string request, string result, string event_timestamp)
        {
            EventName = "screen_tracking";
            EventParameters.Add(nameof(previous_screen_id),new FirebaseAnalyticsEventParameter(previous_screen_id));
            EventParameters.Add(nameof(current_screen_id),new FirebaseAnalyticsEventParameter(current_screen_id));
            EventParameters.Add(nameof(screen_duration_inseconds),new FirebaseAnalyticsEventParameter(screen_duration_inseconds));
            EventParameters.Add(nameof(request),new FirebaseAnalyticsEventParameter(request));
            EventParameters.Add(nameof(result),new FirebaseAnalyticsEventParameter(result));

            EventParameters.Add(nameof(event_timestamp), new FirebaseAnalyticsEventParameter(event_timestamp));
        }
    }
}