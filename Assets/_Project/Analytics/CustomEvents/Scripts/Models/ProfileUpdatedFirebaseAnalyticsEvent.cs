using _Project.Analytics.FirebaseAnalytics.Scripts.Models;
using _Project.PlayerProfile.Scripts.Services;

namespace _Project.Analytics.CustomEvents.Scripts.Models
{
	public class ProfileUpdatedFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
	{

		//INJECT NEEDED MODELS AND SERVICES HERE. THIS CLEANS OUT REST OF THE CODEBASE AND ISOLATES INJECTIONS TO THIS CLASS. 
		//THIS CLASS IS RESPONSIBLE FOR REACHING OUT ITS OWN DEPENDENCIES. ALL OF THEM DOES NOT HAVE TO BE IN THE CONSTRUCTOR.
		//IF YOU NEED TO CREATE THIS CLASS'S INSTANCE FROM DIFFERENT PLACES WITH DIFFERENT PARAMETERS, SIMPLY OVERLOAD THE CONSTRUCTOR
		//AND FIND MISSING PARAMETERS/DEPENDENCIES WITHIN THIS CLASS.
		[Inject] public IPlayerProfileService PlayerProfileService { get; set; }
		public ProfileUpdatedFirebaseAnalyticsEvent()
		{
			
		}
		public ProfileUpdatedFirebaseAnalyticsEvent SetParametersAndReturn(string is_vip)
		{
			EventName = "profile_updated";
			
			EventParameters.Add(nameof(is_vip), new FirebaseAnalyticsEventParameter(is_vip));
			EventParameters.Add("event_timestamp", new FirebaseAnalyticsEventParameter(DateUtility.GetCurrentEpochSeconds().ToString()));
			
			return this;
		}
	}
}