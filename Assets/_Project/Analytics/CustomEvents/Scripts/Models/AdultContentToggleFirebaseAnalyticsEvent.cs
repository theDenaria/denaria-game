using _Project.Analytics.FirebaseAnalytics.Scripts.Models;
using _Project.PlayerProfile.Scripts.Services;
using JetBrains.Annotations;

namespace _Project.Analytics.CustomEvents.Scripts.Models
{
    public class AdultContentToggleFirebaseAnalyticsEvent : FirebaseAnalyticsEvent
    {
        //INJECT NEEDED MODELS AND SERVICES HERE. THIS CLEANS OUT REST OF THE CODEBASE AND ISOLATES INJECTIONS TO THIS CLASS. 
        //THIS CLASS IS RESPONSIBLE FOR REACHING OUT ITS OWN DEPENDENCIES. ALL OF THEM DOES NOT HAVE TO BE PROVIDED IN SetParametersAndReturn.
        //IF YOU NEED TO CREATE THIS CLASS'S INSTANCE FROM DIFFERENT PLACES WITH DIFFERENT PARAMETERS, SIMPLY OVERLOAD SetParametersAndReturn METHOD.
        //AND FIND MISSING PARAMETERS/DEPENDENCIES WITHIN THIS CLASS.
        
        [Inject] public IPlayerProfileService PlayerProfileService { get; set; }

        public AdultContentToggleFirebaseAnalyticsEvent()
        {
            
        }
        
        public AdultContentToggleFirebaseAnalyticsEvent SetParametersAndReturn(bool isToggleOn, [CanBeNull] string eventName = null)
        {
            EventName = eventName;
            long isToggleOnLong = isToggleOn ? 1 : 0;
            EventParameters.Add(nameof(isToggleOn), new FirebaseAnalyticsEventParameter(isToggleOnLong));
            
            Debug.Log("XXXXX " + PlayerProfileService.GetPlayerName() + " " + PlayerProfileService.GetPlayerProfileId());

            return this;
        }
    }
}