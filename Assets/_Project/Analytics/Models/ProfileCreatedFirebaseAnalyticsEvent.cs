using _Project.PlayerProfile.Scripts.Services;

namespace _Project.Analytics.Models
{
	public class ProfileCreatedFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
	{
		[Inject] public IPlayerProfileService PlayerProfileService { get; set; }
		public ProfileCreatedFirebaseAnalyticsEvent()
		{
			
		}
		
		public ProfileCreatedFirebaseAnalyticsEvent SetParametersAndReturn()
		{
			EventName = "profile_created";

			EventParameters.Add("event_timestamp", new FirebaseAnalyticsEventParameter(DateUtility.GetCurrentEpochSeconds().ToString()));

			return this;
		}
	}
}
