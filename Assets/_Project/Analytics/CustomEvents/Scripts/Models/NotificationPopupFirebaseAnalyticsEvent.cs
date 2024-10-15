using _Project.Analytics.FirebaseAnalytics.Scripts.Models;

namespace _Project.Analytics.CustomEvents.Scripts.Models
{
    public class NotificationPopupFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
    {
        public NotificationPopupFirebaseAnalyticsEvent()
        {
            
        }

        public NotificationPopupFirebaseAnalyticsEvent SetParametersAndReturn(string result)
        {

            EventName = "notification_popup";
            
            EventParameters.Add(nameof(result), new FirebaseAnalyticsEventParameter(result));
            EventParameters.Add("event_timestamp", new FirebaseAnalyticsEventParameter(DateUtility.GetCurrentEpochSeconds().ToString()));

            return this;
        }
        
        public NotificationPopupFirebaseAnalyticsEvent SetParametersAndReturn(string result, string event_timestamp)
        {

            EventName = "notification_popup";
            
            EventParameters.Add(nameof(result), new FirebaseAnalyticsEventParameter(result));
            EventParameters.Add(nameof(event_timestamp), new FirebaseAnalyticsEventParameter(event_timestamp));

            return this;
        }
    }
}