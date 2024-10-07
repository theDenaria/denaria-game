using System;

namespace _Project.Analytics.Models
{
	public class PushNotificationTrackingFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
	{
		public PushNotificationTrackingFirebaseAnalyticsEvent(string status,DateTime eventTargetTime)
		{
			EventName = "push_notification_tracking";

			EventParameters.Add(nameof(status),new FirebaseAnalyticsEventParameter(status));

			DateTimeOffset dateTimeOffset = new DateTimeOffset(eventTargetTime);
			long targetEpochSeconds = dateTimeOffset.ToUnixTimeSeconds();

			EventParameters.Add("event_timestamp", new FirebaseAnalyticsEventParameter(targetEpochSeconds.ToString()));
		}
	}
}